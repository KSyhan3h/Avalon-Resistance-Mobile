using System;

namespace AvalonServerPlugin.Scripts.Networking
{
	[Flags]
	public enum StateTag
	{
		None = 0,
		// Network-based information
		Network = 1,
		Connected = 2,
		Disconnected = 4,


		// Player-based information
		Settings = 10,
		UpdatePlayerInfo = 11,


		// Lobby-based information
		Lobby = 20,
		UpdateLobbyInfo = 21,
		GetNextLobbyPage = 22,
		GetPreviousLobbyPage = 23,


		// Room-based information
		Room = 30,
		AddPlayerToRoom = 31,
		RemovePlayerToRoom = 32,

		UnableToJoin = 40,
		RoomFull = 41,
		IncorrectPassword = 42,

		Open = 50,
		Closed = 51,
		InGame = 52,
		Public = 53,
		Private = 54,


		// Game-based information
		Game = 60,
		StartGame = 61,

		PartySelection = 70,				
		PresentPartySelection = 71,
		PartyApprovalSubmitVote = 72,
		PartyApprovalTally = 73,

		QuestCompletion = 80,
		QuestCompletionSubmitVote = 81,
		QuestCompletionTally = 82,
		QuestCompletionDone = 83,

		EndGame = 90,
		Assassination = 91,
		AssassinationVote = 92,
		
		EndResult = 100,
		Win = 101,							// Will also be an alternative for Approve
		Tie = 102,
		Loss = 103,							// Will also be an alternative for Disapprove


		// Database-based information
		Database,
		// Upload something
		// Download something
	}

	public enum ClientMessageTag
	{
		Player,
		Lobby,
		Room,
		Chat,
		Game
	}

	public enum LobbyMessageTag
	{
		UpdateLobby
	}

	
}
