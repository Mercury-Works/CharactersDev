using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using JetBrains.Annotations;
using MercuryWorks.Characters.Client.Models;
using MercuryWorks.Characters.Client.Overlays;
using MercuryWorks.Characters.Shared;
using MercuryWorks.Characters.Shared.Models.Dto;
using NFive.SDK.Client.Commands;
using NFive.SDK.Client.Events;
using NFive.SDK.Client.Extensions;
using NFive.SDK.Client.Input;
using NFive.SDK.Client.Interface;
using NFive.SDK.Client.Rpc;
using NFive.SDK.Client.Services;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Style = MercuryWorks.Characters.Client.Models.Style;
using Vector3 = CitizenFX.Core.Vector3;

namespace MercuryWorks.Characters.Client
{
	[PublicAPI]
	public class CharactersService : Service
	{
		private bool started = false;
		private bool isPlaying;
		private Configuration config;
		private Control activateKey;
		private CharacterOverlay overlay;
		private CharacterSession session;
		private Character activeCharacter;

		public CharactersService(ILogger logger, ITickManager ticks, IEventManager events, IRpcHandler rpc, ICommandManager commands, OverlayManager overlay, User user) : base(logger, ticks, events, rpc, commands, overlay, user) { }

		public override async Task Started()
		{
			// Request server configuration
			this.config = await this.Rpc.Event(CharacterEvents.Configuration).Request<Configuration>();

			this.activateKey = (Control)Enum.Parse(typeof(Control), this.config.SelectionScreen.Hotkey, true);
		}

		public override async Task HoldFocus()
		{
			// Hide HUD
			Screen.Hud.IsVisible = false;

			// Disable the loading screen from automatically being dismissed
			API.SetManualShutdownLoadingScreenNui(true);

			// Position character, required for switching
			Game.Player.Character.Position = Vector3.Zero;

			// Freeze
			Game.Player.Freeze();

			// Switch out the player if it isn't already in a switch state
			if (!API.IsPlayerSwitchInProgress()) API.SwitchOutPlayer(API.PlayerPedId(), 0, 1);

			// Remove most clouds
			API.SetCloudHatOpacity(0.01f);

			// Wait for switch
			while (API.GetPlayerSwitchState() != 5) await Delay(10);

			// Hide loading screen
			API.ShutdownLoadingScreen();

			// Fade out
			Screen.Fading.FadeOut(0);
			while (Screen.Fading.IsFadingOut) await Delay(10);

			// Get characters
			var characters = await this.Rpc.Event(CharacterEvents.Load).Request<List<Character>>();

			// Show overlay
			this.overlay = new CharacterOverlay(characters, this.OverlayManager);
			this.overlay.Create += OnCreate;
			this.overlay.Disconnect += OnDisconnect;
			this.overlay.Select += OnSelect;
			this.overlay.Delete += OnDelete;

			// Focus overlay
			API.SetNuiFocus(true, true);

			// Shut down the NUI loading screen
			API.ShutdownLoadingScreenNui();

			// Fade in
			Screen.Fading.FadeIn(500);
			while (Screen.Fading.IsFadingIn) await Delay(10);

			// Wait for user before releasing focus
			while (!this.started) await Delay(20);
		}

		private async void OnCreate(object sender, CreateOverlayEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(e.Character.Middlename)) e.Character.Middlename = null;

			//e.Character.WalkingStyle = "move_m@drunk@verydrunk";
			e.Character.WalkingStyle = (e.Character.Gender == 0 ? "move_m@generic" : "move_f@generic");
			e.Character.Model = ((uint)(e.Character.Gender == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01)).ToString();

			// Send new character
			var character = await this.Rpc.Event(CharacterEvents.Create).Request<Character>(e.Character);

			this.session = await this.Rpc.Event(CharacterEvents.Select).Request<CharacterSession>(character.Id);
			await Play(e.Overlay, character);
		}

		private void OnDisconnect(object sender, OverlayEventArgs e)
		{
			this.Rpc.Event(SessionEvents.DisconnectPlayer).Trigger("Thanks for playing");
		}

		private async void OnSelect(object sender, IdOverlayEventArgs e)
		{
			this.session = await this.Rpc.Event(CharacterEvents.Select).Request<CharacterSession>(e.Id);
			await Play(e.Overlay, this.overlay.Characters.First(c => c.Id == e.Id));
		}

		private async void OnDelete(object sender, IdOverlayEventArgs e)
		{
			this.Logger.Debug($"Delete {e.Id}");

			this.overlay.Characters = await this.Rpc.Event(CharacterEvents.Delete).Request<List<Character>>(e.Id);

			this.overlay.SyncCharacters();
		}

		private async Task Play(Overlay o, Character character)
		{
			await this.Events.RaiseAsync(CharacterEvents.CharacterIntercomPlay, character.Id);

			// Destroy overlay
			o.Dispose();

			// Un-focus overlay
			API.SetNuiFocus(false, false);

			// Load and render character model
			while (!await Game.Player.ChangeModel(new Model(character.ModelHash))) await Delay(10);
			character.RenderCustom(this.Logger);
			await character.Render(this.Logger);

			// Unfreeze
			Game.Player.Unfreeze();

			// Show HUD
			Screen.Hud.IsVisible = true;

			// Switch in
			API.SwitchInPlayer(API.PlayerPedId());

			// Set character as active character
			this.activeCharacter = character;

			// Set as playing
			this.isPlaying = true;

			// Set player health (Rare #OnSpawnDeath Fix)
			this.activeCharacter.Health = character.Health;

			// Attach tick handlers after character selection
			// to reduce character select click lag
			this.Ticks.Attach(OnHotkey);
			//this.Ticks.Attach(OnSaveCharacter);
			this.Ticks.Attach(OnSavePosition);

			// Release focus hold
			this.started = true;
		}

		public async Task OnHotkey()
		{
			if (!Input.IsControlJustPressed(this.activateKey)) return;

			SaveCharacter();

			#region hotkey

			/*

			// Set as playing
			this.isPlaying = false;

			// Hide HUD
			Screen.Hud.IsVisible = false;

			// Remove most clouds
			API.SetCloudHatOpacity(0.01f);

			// Switch out the player if it isn't already in a switch state
			if (!API.IsPlayerSwitchInProgress()) API.SwitchOutPlayer(API.PlayerPedId(), 0, 1);

			// Wait for switch
			while (API.GetPlayerSwitchState() != 5) await Delay(10);

			// Freeze
			Game.Player.Freeze();

			// Fade out
			Screen.Fading.FadeOut(1000);
			while (Screen.Fading.IsFadingOut) await Delay(10);

			// Position character, required for switching
			Game.Player.Character.Position = Vector3.Zero;

			// Get characters
			var characters = await this.Rpc.Event(CharacterEvents.Load).Request<List<Character>>();

			// Show overlay
			this.overlay = new CharacterOverlay(characters, this.OverlayManager);
			this.overlay.Create += OnCreate;
			this.overlay.Disconnect += OnDisconnect;
			this.overlay.Select += OnSelect;
			this.overlay.Delete += OnDelete;

			// Focus overlay
			API.SetNuiFocus(true, true);

			// Fade in
			Screen.Fading.FadeIn(500);
			while (Screen.Fading.IsFadingIn) await Delay(10);

			this.Ticks.Detach(OnHotkey);
			*/

			#endregion
		}

		public async Task OnSaveCharacter()
		{
			SaveCharacter();

			await Delay(this.config.Autosave.CharacterInterval);
		}

		public async Task OnSavePosition()
		{
			SavePosition();

			await Delay(this.config.Autosave.PositionInterval);
		}

		private void SaveCharacter()
		{
			if (!this.isPlaying) return;

			this.SaveStyle();
			this.SavePosition();

			var pos = Game.Player.Character.Position;

			var characterDto = new CharacterSaveDto
			{
				Gender = (short)(Game.Player.Character.Gender == Gender.Male ? 0 : 1),
				Alive = Game.Player.Character.IsAlive,
				Health = Game.Player.Character.Health,
				Armor = Game.Player.Character.Armor,
				Position = new Position(pos.X, pos.Y, pos.Z),
				Model = ((uint)Game.Player.Character.Model.Hash).ToString()
			};

			this.Logger.Debug($"Current model hash: {this.activeCharacter.ModelHash}");


			if (this.activeCharacter.ModelHash != PedHash.FreemodeMale01 &&
				this.activeCharacter.ModelHash != PedHash.FreemodeFemale01) return;
			this.Logger.Debug($"Saving mp freemode player");

			this.SaveAppearance();
			this.SaveFacialFeature();
			this.SaveHeritage();

			#region SaveChar

			//var pos = Game.Player.Character.Position;

			//this.activeCharacter.Position = new Position(pos.X, pos.Y, pos.Z);
			//this.Rpc.Event(CharacterEvents.SaveCharacter).Trigger(this.activeCharacter);

			#endregion
		}

		private void SaveStyle()
		{
			var style = Game.Player.Character.Style;
			var saveStyle = Style.ConvertStyle(style, null);

			this.Rpc.Event(CharacterEvents.SaveApparel).Trigger(this.activeCharacter.Id, saveStyle);
		}

		private void SaveAppearance()
		{
			var saveAppearance = Appearance.ConvertAppearance(Game.PlayerPed);

			this.Rpc.Event(CharacterEvents.SaveAppearance).Trigger(this.activeCharacter.Id, saveAppearance);
		}

		private void SaveHeritage()
		{
			var headBlendData = Game.PlayerPed.GetHeadBlendData();

			var saveHeritage = new Heritage
			{
				Created = this.activeCharacter.Heritage.Created,
				Parent1 = headBlendData.FirstFaceShape,
				Parent2 = headBlendData.SecondFaceShape,
				Resemblance = headBlendData.ParentFaceShapePercent,
				SkinTone = headBlendData.ParentSkinTonePercent
			};

			this.Logger.Debug($"Heritage Save: {saveHeritage}");

			this.Rpc.Event(CharacterEvents.SaveHeritage).Trigger(this.activeCharacter.Id, saveHeritage);
		}

		private void SaveFacialFeature()
		{
			var player = Game.Player.Character.Handle;

			var saveFacialFeature = new FacialFeature
			{
				NoseWidth = API.GetPedFaceFeature(player, 0),
				NosePeakHeight = API.GetPedFaceFeature(player, 1),
				NosePeakLength = API.GetPedFaceFeature(player, 2),
				NoseBoneHeight = API.GetPedFaceFeature(player, 3),
				NosePeakLowering = API.GetPedFaceFeature(player, 4),
				NoseBoneTwist = API.GetPedFaceFeature(player, 5),
				EyeBrowHeight = API.GetPedFaceFeature(player, 6),
				EyeBrowLength = API.GetPedFaceFeature(player, 7),
				CheekBoneHeight = API.GetPedFaceFeature(player, 8),
				CheekBoneWidth = API.GetPedFaceFeature(player, 9),
				CheekWidth = API.GetPedFaceFeature(player, 10),
				EyeOpenings = API.GetPedFaceFeature(player, 11),
				LipThickness = API.GetPedFaceFeature(player, 12),
				JawBoneWidth = API.GetPedFaceFeature(player, 13),
				JawBoneLength = API.GetPedFaceFeature(player, 14),
				ChinBoneLowering = API.GetPedFaceFeature(player, 15),
				ChinBoneLength = API.GetPedFaceFeature(player, 16),
				ChinBoneWidth = API.GetPedFaceFeature(player, 17),
				ChinDimple = API.GetPedFaceFeature(player, 18),
				NeckThickness = API.GetPedFaceFeature(player, 19),
			};

			this.Rpc.Event(CharacterEvents.SaveFacialFeature).Trigger(this.activeCharacter.Id, saveFacialFeature);
		}

		private void SavePosition()
		{
			if (!this.isPlaying) return;

			var pos = Game.Player.Character.Position;

			this.Rpc.Event(CharacterEvents.SavePosition).Trigger(this.activeCharacter.Id, new Position(pos.X, pos.Y, pos.Z));
		}
	}
}
