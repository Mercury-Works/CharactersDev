using JetBrains.Annotations;
using NFive.SDK.Server.Migrations;
using MercuryWorks.Characters.Server.Storage;

namespace MercuryWorks.Characters.Server.Migrations
{
	[UsedImplicitly]
	public sealed class Configuration : MigrationConfiguration<StorageContext> { }
}
