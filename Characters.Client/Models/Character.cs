using CitizenFX.Core;
using CitizenFX.Core.Native;
using MercuryWorks.Characters.Shared.Models;
using Newtonsoft.Json;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Models;
using System;
using System.Threading.Tasks;

namespace MercuryWorks.Characters.Client.Models
{
	public class Character : IdentityModel, ICharacter
	{
		public string Forename { get; set; }
		public string Middlename { get; set; }
		public string Surname { get; set; }
		public DateTime DateOfBirth { get; set; }
		public short Gender { get; set; }
		public bool Alive { get; set; }
		public int Health { get; set; }
		public int Armor { get; set; }
		public int Ssn { get; set; }
		public Position Position { get; set; }
		public string Model { get; set; }
		public string WalkingStyle { get; set; }
		public Guid StyleId { get; set; }
		public Style Style { get; set; }
		public Guid AppearanceId { get; set; }
		public Appearance Appearance { get; set; }
		public Guid FaceShapeId { get; set; }
		public FacialFeature FacialFeature { get; set; }
		public Guid HeritageId { get; set; }
		public Heritage Heritage { get; set; }
		public DateTime? LastPlayed { get; set; }
		public Guid UserId { get; set; }

		[JsonIgnore] public string FullName => $"{this.Forename} {this.Middlename} {this.Surname}".Replace("  ", " ");

		[JsonIgnore] public PedHash ModelHash => (PedHash)Convert.ToUInt32(this.Model);


		public void RenderCustom(ILogger logger)
		{
			// Only for free mode models
			if (this.ModelHash != PedHash.FreemodeMale01 && this.ModelHash != PedHash.FreemodeFemale01) return;

			var player = Game.Player.Character.Handle;

			// https://gtaforums.com/topic/858970-all-gtao-face-ids-pedset_ped_head_blend_data-explained/
			// https://wiki.gt-mp.net/index.php/Character_Components
			// https://wiki.gt-mp.net/index.php?title=Hair_Colors

			API.SetPedHeadBlendData(player, this.Heritage.Parent1, this.Heritage.Parent2, 0, this.Heritage.Parent1, this.Heritage.Parent2, 0, this.Heritage.Resemblance, this.Heritage.SkinTone, 0f, true);

			// Eye Color
			API.SetPedEyeColor(player, this.Appearance.EyeColorId);

			// Hair Color
			API.SetPedHairColor(player, this.Appearance.HairColorId, this.Appearance.HairHighlightColor);

			// 0 Blemishes
			API.SetPedHeadOverlay(player, 0, this.Appearance.Blemishes.Index, this.Appearance.Blemishes.Opacity);

			// 1 Beard
			API.SetPedHeadOverlay(player, 1, this.Appearance.Beard.Index, this.Appearance.Beard.Opacity);
			API.SetPedHeadOverlayColor(player, 1, 1, this.Appearance.Beard.ColorId, this.Appearance.Beard.SecondColorId);

			// 2 EyeBrows
			API.SetPedHeadOverlay(player, 2, this.Appearance.Eyebrows.Index, this.Appearance.Eyebrows.Opacity);
			API.SetPedHeadOverlayColor(player, 2, 1, this.Appearance.Eyebrows.ColorId, this.Appearance.Eyebrows.SecondColorId);

			// 3 Aging
			API.SetPedHeadOverlay(player, 3, this.Appearance.Aging.Index, this.Appearance.Aging.Opacity);

			// 4 MakeUp
			API.SetPedHeadOverlay(player, 4, this.Appearance.Makeup.Index, this.Appearance.Makeup.Opacity);
			API.SetPedHeadOverlayColor(player, 4, 2, this.Appearance.Makeup.ColorId, this.Appearance.Makeup.SecondColorId);

			// 5 Blush
			API.SetPedHeadOverlay(player, 5, this.Appearance.Blush.Index, this.Appearance.Blush.Opacity);
			API.SetPedHeadOverlayColor(player, 5, 2, this.Appearance.Blush.ColorId, this.Appearance.Blush.SecondColorId);

			// 6 Complexion
			API.SetPedHeadOverlay(player, 6, this.Appearance.Complexion.Index, this.Appearance.Complexion.Opacity);

			// 7 Sun Damage
			API.SetPedHeadOverlay(player, 7, this.Appearance.SunDamage.Index, this.Appearance.SunDamage.Opacity);

			// 8 Lipstick
			API.SetPedHeadOverlay(player, 8, this.Appearance.Lipstick.Index, this.Appearance.Lipstick.Opacity);
			API.SetPedHeadOverlayColor(player, 8, 2, this.Appearance.Lipstick.ColorId, this.Appearance.Lipstick.SecondColorId);

			// 9 Moles And Freckles
			API.SetPedHeadOverlay(player, 9, this.Appearance.MolesAndFreckles.Index, this.Appearance.MolesAndFreckles.Opacity);

			// 10 Chest Hair
			API.SetPedHeadOverlay(player, 10, this.Appearance.ChestHair.Index, this.Appearance.ChestHair.Opacity);
			API.SetPedHeadOverlayColor(player, 10, 1, this.Appearance.ChestHair.ColorId, this.Appearance.ChestHair.SecondColorId);

			// 11 Body Blemishes
			API.SetPedHeadOverlay(player, 11, this.Appearance.BodyBlemishes.Index, this.Appearance.BodyBlemishes.Opacity);


			API.SetPedFaceFeature(player, 0, this.FacialFeature.NoseWidth);
			API.SetPedFaceFeature(player, 1, this.FacialFeature.NosePeakHeight);
			API.SetPedFaceFeature(player, 2, this.FacialFeature.NosePeakLength);
			API.SetPedFaceFeature(player, 3, this.FacialFeature.NoseBoneHeight);
			API.SetPedFaceFeature(player, 4, this.FacialFeature.NosePeakLowering);
			API.SetPedFaceFeature(player, 5, this.FacialFeature.NoseBoneTwist);
			API.SetPedFaceFeature(player, 6, this.FacialFeature.EyeBrowHeight);
			API.SetPedFaceFeature(player, 7, this.FacialFeature.EyeBrowLength);
			API.SetPedFaceFeature(player, 8, this.FacialFeature.CheekBoneHeight);
			API.SetPedFaceFeature(player, 9, this.FacialFeature.CheekBoneWidth);
			API.SetPedFaceFeature(player, 10, this.FacialFeature.CheekWidth);
			API.SetPedFaceFeature(player, 11, this.FacialFeature.EyeOpenings);
			API.SetPedFaceFeature(player, 12, this.FacialFeature.LipThickness);
			API.SetPedFaceFeature(player, 13, this.FacialFeature.JawBoneWidth);
			API.SetPedFaceFeature(player, 14, this.FacialFeature.JawBoneLength);
			API.SetPedFaceFeature(player, 15, this.FacialFeature.ChinBoneLowering);
			API.SetPedFaceFeature(player, 16, this.FacialFeature.ChinBoneLength);
			API.SetPedFaceFeature(player, 17, this.FacialFeature.ChinBoneWidth);
			API.SetPedFaceFeature(player, 18, this.FacialFeature.ChinDimple);
			API.SetPedFaceFeature(player, 19, this.FacialFeature.NeckThickness);
		}

		public async Task Render(ILogger logger)
		{
			// Apparently this _must_ be called
			Game.Player.Character.Style.SetDefaultClothes();

			//Game.Player.Character.Position = this.Position.ToVector3();
			Game.Player.Character.Position = new CitizenFX.Core.Vector3(this.Position.X, this.Position.Y, this.Position.Z);
			Game.Player.Character.Armor = this.Armor;

			API.RequestClipSet(this.WalkingStyle);
			await BaseScript.Delay(100); // Required to load
			Game.Player.Character.MovementAnimationSet = this.WalkingStyle;

			Game.Player.Character.Style[PedComponents.Face].SetVariation(this.Style.Face.Index, this.Style.Face.Texture);
			Game.Player.Character.Style[PedComponents.Head].SetVariation(this.Style.Head.Index, this.Style.Head.Texture);

			// Temporary network visibility fix workaround
			Game.Player.Character.Style[PedComponents.Hair].SetVariation(1, 1);

			Game.Player.Character.Style[PedComponents.Hair].SetVariation(this.Style.Hair.Index, this.Style.Hair.Texture);

			Game.Player.Character.Style[PedComponents.Torso].SetVariation(this.Style.Torso.Index, this.Style.Torso.Texture);
			Game.Player.Character.Style[PedComponents.Legs].SetVariation(this.Style.Legs.Index, this.Style.Legs.Texture);
			Game.Player.Character.Style[PedComponents.Hands].SetVariation(this.Style.Hands.Index, this.Style.Hands.Texture);
			Game.Player.Character.Style[PedComponents.Shoes].SetVariation(this.Style.Shoes.Index, this.Style.Shoes.Texture);
			Game.Player.Character.Style[PedComponents.Special1].SetVariation(this.Style.Special1.Index, this.Style.Special1.Texture);
			Game.Player.Character.Style[PedComponents.Special2].SetVariation(this.Style.Special2.Index, this.Style.Special2.Texture);
			Game.Player.Character.Style[PedComponents.Special3].SetVariation(this.Style.Special3.Index, this.Style.Special3.Texture);
			Game.Player.Character.Style[PedComponents.Textures].SetVariation(this.Style.Textures.Index, this.Style.Textures.Texture);
			Game.Player.Character.Style[PedComponents.Torso2].SetVariation(this.Style.Torso2.Index, this.Style.Torso2.Texture);

			Game.Player.Character.Style[PedProps.Hats].SetVariation(this.Style.Hat.Index, this.Style.Hat.Texture);
			Game.Player.Character.Style[PedProps.Glasses].SetVariation(this.Style.Glasses.Index, this.Style.Glasses.Texture);
			Game.Player.Character.Style[PedProps.EarPieces].SetVariation(this.Style.EarPiece.Index, this.Style.EarPiece.Texture);
			Game.Player.Character.Style[PedProps.Unknown3].SetVariation(this.Style.Unknown3.Index, this.Style.Unknown3.Texture);
			Game.Player.Character.Style[PedProps.Unknown4].SetVariation(this.Style.Unknown4.Index, this.Style.Unknown4.Texture);
			Game.Player.Character.Style[PedProps.Unknown5].SetVariation(this.Style.Unknown5.Index, this.Style.Unknown5.Texture);
			Game.Player.Character.Style[PedProps.Watches].SetVariation(this.Style.Watch.Index, this.Style.Watch.Texture);
			Game.Player.Character.Style[PedProps.Wristbands].SetVariation(this.Style.Wristband.Index, this.Style.Wristband.Texture);
			Game.Player.Character.Style[PedProps.Unknown8].SetVariation(this.Style.Unknown8.Index, this.Style.Unknown8.Texture);
			Game.Player.Character.Style[PedProps.Unknown9].SetVariation(this.Style.Unknown9.Index, this.Style.Unknown9.Texture);
		}
	}
}
