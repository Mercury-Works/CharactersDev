using MercuryWorks.Characters.Shared.Models;
using MercuryWorks.Characters.Shared.Models.Appearance;
using NFive.SDK.Core.Helpers;
using NFive.SDK.Core.Models;

namespace MercuryWorks.Characters.Server.Models
{
	public class Appearance : IdentityModel, IAppearance
	{
		public int EyeColorId { get; set; }
		public int HairColorId { get; set; }
		public int HairHighlightColor { get; set; }
		public Feature Blemishes { get; set; }
		public Feature Beard { get; set; }
		public Feature Eyebrows { get; set; }
		public Feature Aging { get; set; }
		public Feature Makeup { get; set; }
		public Feature Blush { get; set; }
		public Feature Complexion { get; set; }
		public Feature SunDamage { get; set; }
		public Feature Lipstick { get; set; }
		public Feature MolesAndFreckles { get; set; }
		public Feature ChestHair { get; set; }
		public Feature BodyBlemishes { get; set; }
		public Feature AddBodyBlemishes { get; set; }

		public Appearance()
		{
			this.Id = GuidGenerator.GenerateTimeBasedGuid();

			this.Blemishes = new Feature();
			this.Beard = new Feature();
			this.Eyebrows = new Feature();
			this.Aging = new Feature();
			this.Makeup = new Feature();
			this.Blush = new Feature();
			this.Complexion = new Feature();
			this.SunDamage = new Feature();
			this.Lipstick = new Feature();
			this.MolesAndFreckles = new Feature();
			this.ChestHair = new Feature();
			this.BodyBlemishes = new Feature();
			this.AddBodyBlemishes = new Feature();
		}
	}
}
