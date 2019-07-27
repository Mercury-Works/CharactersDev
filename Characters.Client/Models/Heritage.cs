using MercuryWorks.Characters.Shared.Models;
using NFive.SDK.Core.Models;

namespace MercuryWorks.Characters.Client.Models
{
	public class Heritage : IdentityModel, IHeritage
	{
		public int Parent1 { get; set; }
		public int Parent2 { get; set; }
		public float Resemblance { get; set; }
		public float SkinTone { get; set; }
	}
}
