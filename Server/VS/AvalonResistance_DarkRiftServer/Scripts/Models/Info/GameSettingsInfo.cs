using System;
using System.Collections.Generic;
using System.Text;

namespace AvalonServerPlugin.Scripts.Models.Info
{
	public class GameSettingsInfo
	{
		public int minPlayers;
		public int maxPlayers;
		public int maxVotes;
		public List<int> quests;

		// Timer Settings

		// Characters present in both factions
		// Characters will be represented with an ID instead,
		// this is so that the server will not need to handle very heavy information
		public List<int> characters;

	}
}
