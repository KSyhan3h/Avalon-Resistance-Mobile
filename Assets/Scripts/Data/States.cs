using System;

namespace AvalonResistance.States
{
	[Flags]
	public enum StateTag
	{ 
		// Network-based information
		Network,
		Connected,
		Disconnected,

		
		// Player-based information
		Settings,
		UpdatePlayerInfo,

		
		// Lobby-based information
		Lobby,
		UpdateLobbyInfo,
		GetNextLobbyPage,
		GetPreviousLobbyPage,

		
		// Room-based information
		Room,
		AddPlayerToRoom,
		RemovePlayerToRoom,
		
		UnableToJoin,
		RoomFull,
		IncorrectPassword,
				
		Open,
		Closed,
		InGame,

		Public,
		Private,


		// Game-based information
		Game,
		RoleAssignment,
		PartySelection,
		QuestCompletion,
		Assassination,
		EndResult,


		// Database-based information
		Database,
		// Upload something
		// Download something
	}
}
