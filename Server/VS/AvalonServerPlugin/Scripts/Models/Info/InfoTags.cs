using System;
using System.Collections.Generic;
using System.Text;

namespace AvalonServerPlugin.Scripts.Models.Info
{
	public enum RoomState
	{
		Open,               // People can join
		Closed,             // Room is not in use
		InGame              // Players cannot join becuae game is ongoing
	}

	public enum GameState
	{
		RoleAssignment,
		PartySelection,
		QuestCompletion,
		Assassination,
		EndResult
	}

	public enum RoomAvailability
	{
		Public,
		Private
	}

}
