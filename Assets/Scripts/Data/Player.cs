using AvalonResistance.Data;

using System;
using System.Runtime.CompilerServices;

namespace AvalonResistance
{
	[Serializable]
	public class Player
	{
		// Connection Data
		public readonly ushort ClientID;
		// Room Data
		public bool isHostClient;
		
		// InGame Data
		public bool isLeader;
		public Character character;
		
		// PlayerProfile
		public PlayerProfile userData;

		public Player (ushort clientID, bool isHostClient = false)
		{
			ClientID = clientID;
			this.isHostClient = isHostClient;
		}
	}
}