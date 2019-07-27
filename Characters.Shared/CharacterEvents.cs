namespace MercuryWorks.Characters.Shared
{
	public static class CharacterEvents
	{
		public const string Configuration = "mercuryworks:characters:configuration";
		public const string Disconnecting = "mercuryworks:characters:disconnecting";

		public const string Create = "mercuryworks:characters:create";
		public const string Load = "mercuryworks:characters:load";
		public const string Delete = "mercuryworks:characters:delete";

		public const string Select = "mercuryworks:characters:select";
		public const string DeselectAll = "mercuryworks:characters:deselectall";

		public const string SaveCharacter = "mercuryworks:characters:save:character";
		public const string SaveApparel = "mercuryworks:characters:save:apparel";
		public const string SaveAppearance = "mercuryworks:characters:save:appearance";
		public const string SaveHeritage = "mercuryworks:characters:save:heritage";
		public const string SaveFacialFeature = "mercuryworks:characters:save:facialfeature";
		public const string SavePosition = "mercuryworks:characters:save:position";

		public const string Selecting = "mercuryworks:characters:selecting";
		public const string Selected = "mercuryworks:characters:selected";
		public const string Deselecting = "mercuryworks:characters:deselecting";
		public const string Deselected = "mercuryworks:characters:deselected";

		public const string GetActive = "mercuryworks:characters:getactive";

		public const string CharacterIntercomPlay = "mercuryworks:characters:intercom:play";
	}
}
