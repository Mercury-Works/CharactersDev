using CitizenFX.Core;
using CitizenFX.Core.Native;
using MercuryWorks.Characters.Shared.Models;
using MercuryWorks.Characters.Shared.Models.Appearance;
using NFive.SDK.Core.Models;

namespace MercuryWorks.Characters.Client.Models
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


		public static Appearance ConvertAppearance(Ped player) => new Appearance
		{
			EyeColorId = API.GetPedEyeColor(player.Handle),
			HairColorId = API.GetPedHairColor(player.Handle),
			HairHighlightColor = API.GetPedHairHighlightColor(player.Handle),

			Blemishes = PedAppearance(player, 0),
			Beard = PedAppearance(player, 1),
			Eyebrows = PedAppearance(player, 2),
			Aging = PedAppearance(player, 3),
			Makeup = PedAppearance(player, 4),
			Blush = PedAppearance(player, 5),
			Complexion = PedAppearance(player, 6),
			SunDamage = PedAppearance(player, 7),
			Lipstick = PedAppearance(player, 8),
			MolesAndFreckles = PedAppearance(player, 9),
			ChestHair = PedAppearance(player, 10),
			BodyBlemishes = PedAppearance(player, 11),
			AddBodyBlemishes = PedAppearance(player, 12)
		};


		private static Feature PedAppearance(Ped ped, int index)
		{
			var overlayValue = 0;
			var colorType = 0;
			var firstColor = 0;
			var secondColor = 0;
			var opacity = 0.0f;

			API.GetPedHeadOverlayData(ped.Handle, index, ref overlayValue, ref colorType, ref firstColor, ref secondColor, ref opacity);

			var feature = new Feature
			{
				Type = (FeatureTypes)index,
				Index = overlayValue,
				Opacity = opacity,
				ColorType = (FeatureColorTypes)colorType,
				ColorId = firstColor,
				SecondColorId = secondColor
			};

			return feature;
		}
	}
}
