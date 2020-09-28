using System.Collections.Generic;

namespace AvalonServerPlugin.Scripts
{
	public class GameSettingsInfo
	{
		public ushort minPlayers;		// default min = 5		1 merlin ; 2 good ; 2 bad
		public ushort maxPlayers;		// max players = 15
		public ushort maxVotes;
		public List<ushort> quests;		// number of quests and their respective number of members per quest

		// Timer Settings

		// Characters present in both factions
		// Characters will be represented with an ID instead,
		// this is so that the server will not need to handle very heavy information
		public List<int> characters;

	}
}
