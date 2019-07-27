using System.Data.Entity;
using MercuryWorks.Characters.Server.Models;
using NFive.SDK.Core.Models.Player;
using NFive.SDK.Server.Storage;

namespace MercuryWorks.Characters.Server.Storage
{
	public class StorageContext : EFContext<StorageContext>
	{
		public DbSet<Character> Characters { get; set; }

		// DbSet<Apparel> Appearances { get; set; }

		public DbSet<CharacterSession> CharacterSessions { get; set; }
	}
}
