using System.Collections.Generic;

namespace AvalonServerPlugin.Scripts.Models.Info
{
	public class GameSettingsInfo
	{
		public ushort minPlayers;
		public ushort maxPlayers;
		public ushort maxVotes;
		public List<ushort> quests;

		// Timer Settings

		// Characters present in both factions
		// Characters will be represented with an ID instead,
		// this is so that the server will not need to handle very heavy information
		public List<int> characters;

	}
}
