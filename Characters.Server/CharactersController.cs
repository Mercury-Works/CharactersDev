using JetBrains.Annotations;
using MercuryWorks.Characters.Server.Models;
using MercuryWorks.Characters.Server.Storage;
using MercuryWorks.Characters.Shared;
using NFive.SDK.Core.Diagnostics;
using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;
using NFive.SDK.Core.Rpc;
using NFive.SDK.Server.Controllers;
using NFive.SDK.Server.Events;
using NFive.SDK.Server.Rcon;
using NFive.SDK.Server.Rpc;
using NFive.SDK.Server.Wrappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace MercuryWorks.Characters.Server
{
	[PublicAPI]
	public class CharactersController : ConfigurableController<Configuration>
	{
		private readonly SessionManager sessions;

		private readonly List<CharacterSession> characterSessions = new List<CharacterSession>();

		public CharactersController(ILogger logger, IEventManager events, IRpcHandler rpc, IRconManager rcon, Configuration configuration) : base(logger, events, rpc, rcon, configuration)
		{
			// Send configuration when requested
			this.Rpc.Event(CharacterEvents.Configuration).On(e => e.Reply(this.Configuration));
			this.Rpc.Event(CharacterEvents.Load).On(Load);
			this.Rpc.Event(CharacterEvents.Create).On<Character>(Create);
			this.Rpc.Event(CharacterEvents.Delete).On<Guid>(Delete);
			this.Rpc.Event(CharacterEvents.Select).On<Guid>(Select);
			this.Rpc.Event(CharacterEvents.SaveCharacter).On<Character>(SaveCharacter);
			this.Rpc.Event(CharacterEvents.SavePosition).On<Guid, Position>(SavePosition);
			this.Rpc.Event(CharacterEvents.SaveHeritage).On<Guid, Heritage>(SaveHeritage);
			this.Rpc.Event(CharacterEvents.SaveFacialFeature).On<Guid, FacialFeature>(SaveFacialFeature);
			this.Rpc.Event(CharacterEvents.SaveAppearance).On<Guid, Appearance>(SaveAppearance);
			this.Rpc.Event(CharacterEvents.SaveApparel).On<Guid, Style>(SaveApparel);

			this.Events.OnRequest(CharacterEvents.GetActive, () => this.characterSessions);

			// Listen for NFive SessionManager plugin events
			this.sessions = new SessionManager(this.Events, this.Rpc);
			this.sessions.ClientDisconnected += OnClientDisconnected;

			Cleanup();
		}

		private void Cleanup()
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				var activeSessions = context.CharacterSessions.Where(s => s.Disconnected == null).ToList();
				var lastServerActiveTime = this.Events.Request<DateTime?>(BootEvents.GetLastActiveTime) ?? DateTime.UtcNow;
				foreach (var characterSession in activeSessions)
				{
					characterSession.Connected = null;
					characterSession.Disconnected = lastServerActiveTime;
					context.CharacterSessions.AddOrUpdate(characterSession);
				}

				context.SaveChanges();
				transaction.Commit();
			}
		}

		private async void OnClientDisconnected(object sender, ClientSessionEventArgs e)
		{
			await DeselectAll(e.Session.UserId);
		}

		public async Task DeselectAll(Guid id)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				var activeSessions = context.CharacterSessions.Where(s =>
					s.Character.UserId == id
					&& s.Disconnected == null
				).ToList();

				foreach (var characterSession in activeSessions)
				{
					await this.Events.RaiseAsync(CharacterEvents.Deselecting, characterSession);
					characterSession.Connected = null;
					characterSession.Disconnected = DateTime.UtcNow;
					context.CharacterSessions.AddOrUpdate(characterSession);
				}

				await context.SaveChangesAsync();
				transaction.Commit();

				foreach (var characterSession in activeSessions)
				{
					this.characterSessions.RemoveAll(c => c.Id == characterSession.Id);
					await this.Events.RaiseAsync(CharacterEvents.Deselected, characterSession);
				}
			}
		}

		public async void Delete(IRpcEvent e, Guid id)
		{
			using (var context = new StorageContext())
			{
				var character = context.Characters.First(c => c.Id == id);

				character.Deleted = DateTime.UtcNow;

				await context.SaveChangesAsync();

				Load(e);
			}
		}

		public async void Select(IRpcEvent e, Guid id)
		{
			await DeselectAll(e.User.Id);

			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				var character = context.Characters.Include(c => c.User).FirstOrDefault(c => c.Id == id);

				if (character == null)
				{
					e.Reply(null);
					throw new Exception($"No character found for Id \"{id}\""); // TODO: CharacterException
				}
				await this.Events.RaiseAsync(CharacterEvents.Selecting, character);

				var newSession = new CharacterSession
				{
					Id = GuidGenerator.GenerateTimeBasedGuid(),
					CharacterId = character.Id,
					Character = character,
					Created = DateTime.UtcNow,
					Connected = DateTime.UtcNow,
					SessionId = e.Session.Id
				};

				context.CharacterSessions.Add(newSession);

				await context.SaveChangesAsync();
				transaction.Commit();

				this.Logger.Debug("Created character session");
				this.Logger.Debug($"Session: {new Serializer().Serialize(e.Session)}");

				newSession.Session = e.Session;

				e.Reply(newSession);

				this.characterSessions.Add(newSession);

				await this.Events.RaiseAsync(CharacterEvents.Selected, newSession);
			}
		}

		public void Load(IRpcEvent e)
		{
			using (var context = new StorageContext())
			{
				var characters = context.Characters.Where(c => c.Deleted == null && c.UserId == e.User.Id).ToList();

				e.Reply(characters);
			}
		}

		public async void Create(IRpcEvent e, Character character)
		{
			// TODO: Validate client sent values

			// Don't trust important values from clients
			character.Id = GuidGenerator.GenerateTimeBasedGuid();
			character.UserId = e.User.Id;
			character.Alive = true;
			character.Health = 10000;
			character.Armor = 0;
			character.Ssn = Character.GenerateSsn();
			character.LastPlayed = DateTime.UtcNow;
			character.Position = new Position(0f, 0f, 71f);
			character.Style = new Style();
			character.Appearance = new Appearance();
			character.FacialFeature = new FacialFeature();
			character.Heritage = new Heritage();

			// Save character
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					context.Characters.Add(character);

					await context.SaveChangesAsync();
					transaction.Commit();

					this.Logger.Debug($"Saved new character: {character.FullName}");

					// Send back updated user
					e.Reply(character);
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex);

					transaction.Rollback();

					// TODO: Reply with an error so client doesn't hang
				}
			}
		}

		public async void SaveCharacter(IRpcEvent e, Character character)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					context.Characters.AddOrUpdate(character);

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex);

					transaction.Rollback();
				}

				var activeSession = this.characterSessions.FirstOrDefault(s => s.Character.Id == character.Id);
				if (activeSession == null) return;
				activeSession.Character = character;
			}
		}

		public async void SavePosition(IRpcEvent e, Guid characterGuid, Position position)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var saveCharacter = context.Characters.Single(c => c.Id == characterGuid);
					saveCharacter.Position = position;

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex);

					transaction.Rollback();
				}

				var activeSession = this.characterSessions.FirstOrDefault(s => s.Character.Id == characterGuid);
				if (activeSession == null) return;
				activeSession.Character.Position = position;
			}
		}

		private async void SaveHeritage(IRpcEvent e, Guid characterId, Heritage heritage)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var save = context.Characters.Include(c => c.Heritage).Single(c => c.Id == characterId);

					heritage.Id = save.HeritageId;

					context.Entry(save.Heritage).CurrentValues.SetValues(heritage);

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex, "Character Heritage Save");

					transaction.Rollback();
				}
			}
		}

		private async void SaveFacialFeature(IRpcEvent e, Guid characterId, FacialFeature facialFeature)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var save = context.Characters.Include(c => c.FacialFeature).Single(c => c.Id == characterId);

					facialFeature.Id = save.FacialFeature.Id;

					context.Entry(save.FacialFeature).CurrentValues.SetValues(facialFeature);

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex, "Character Facial Features Save");

					transaction.Rollback();
				}
			}
		}

		private async void SaveAppearance(IRpcEvent e, Guid characterId, Appearance appearance)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var save = context.Characters.Include(c => c.Appearance).Single(c => c.Id == characterId);

					appearance.Id = save.AppearanceId;

					context.Entry(save.Appearance).CurrentValues.SetValues(appearance);

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex, "Character Appearance Save");

					transaction.Rollback();
				}
			}
		}


		private async void SaveApparel(IRpcEvent e, Guid characterId, Style apparel)
		{
			using (var context = new StorageContext())
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					var save = context.Characters.Include(c => c.Style).Single(c => c.Id == characterId);

					apparel.Id = save.StyleId;

					context.Entry(save.Style).CurrentValues.SetValues(apparel);

					await context.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					this.Logger.Error(ex, "Character Apparel Save");

					transaction.Rollback();
				}
			}
		}
	}
}
