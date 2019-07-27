using NFive.SDK.Core.Models;

namespace MercuryWorks.Characters.Shared.Models.Dto
{
	public class CharacterSaveDto
	{
		public short Gender { get; set; }
		public bool Alive { get; set; }
		public int Health { get; set; }
		public int Armor { get; set; }
		public Position Position { get; set; }
		public string Model { get; set; }
	}
}
