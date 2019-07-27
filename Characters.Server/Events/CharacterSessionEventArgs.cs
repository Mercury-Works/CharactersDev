using JetBrains.Annotations;
using MercuryWorks.Characters.Server.Models;
using System;

namespace MercuryWorks.Characters.Server.Events
{
	[PublicAPI]
	public class CharacterSessionEventArgs : EventArgs
	{
		public CharacterSession CharacterSession { get; }

		public CharacterSessionEventArgs(CharacterSession session)
		{
			this.CharacterSession = session;
		}
	}
}
