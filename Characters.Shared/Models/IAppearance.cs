using MercuryWorks.Characters.Shared.Models.Appearance;
using NFive.SDK.Core.Models;

namespace MercuryWorks.Characters.Shared.Models
{
	public interface IAppearance : IIdentityModel
	{
		int EyeColorId { get; set; }
		int HairColorId { get; set; }
		int HairHighlightColor { get; set; }

		Feature Blemishes { get; set; }
		Feature Beard { get; set; }
		Feature Eyebrows { get; set; }
		Feature Aging { get; set; }
		Feature Makeup { get; set; }
		Feature Blush { get; set; }
		Feature Complexion { get; set; }
		Feature SunDamage { get; set; }
		Feature Lipstick { get; set; }
		Feature MolesAndFreckles { get; set; }
		Feature ChestHair { get; set; }
		Feature BodyBlemishes { get; set; }
		Feature AddBodyBlemishes { get; set; }
	}
}
