using JetBrains.Annotations;
using MercuryWorks.Characters.Server.Models;
using System;

namespace MercuryWorks.Characters.Server.Events
{
	[PublicAPI]
	public class CharacterEventArgs : EventArgs
	{
		public Character Character { get; }

		public CharacterEventArgs(Character character)
		{
			this.Character = character;
		}
	}
}
