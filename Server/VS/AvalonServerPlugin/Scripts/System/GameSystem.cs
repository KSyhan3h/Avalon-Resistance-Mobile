using System;
using System.Linq;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Server;
using AvalonServerPlugin.Scripts.Models.Info;
using AvalonServerPlugin.Scripts.Networking;

/// <summary>
/// Server side will handle the shuffling and other non-client/player should do
/// This class is created to ensure that there are no biases or information tampering
/// from the client's side
/// 
/// Also, this class will only handle ingame rooms
/// 
/// NOTE: This is an experimental class to be tried out on the real server later 
/// If in that the server cannot handle this much information
/// Players (hosts) will handle the information and process it as well
/// </summary>

namespace AvalonServerPlugin.Scripts.System
{
	class GameSystem
	{
		// Singleton 
		public static GameSystem instance { get; private set; }


		// This class will handle all information to be handled 
		// This class will monitor everything that is happening 

		private Logger _logger;

		// We will need a disctionary to handle information for each room
		// RoomIDs will be the key and we can create a custom class for the whole room information
		private Dictionary<ushort, GameInfo> gameInfoDictionary;

		public GameSystem (Logger logger)
		{
			if (instance != null)
			{
				logger.Error ("There is an existing instance of GameSystem", new Exception ("Duplicate instance of GameSystem"));
				return;
			}

			instance = this;
			_logger = logger;
			gameInfoDictionary = new Dictionary<ushort, GameInfo> ();
		}

		private void InitializeGame (ushort roomID)
		{
			// Check if roomID is already part of the ingame rooms
			if (gameInfoDictionary.ContainsKey (roomID))
			{
				_logger.Error ("Cannot initiatlize room. Room is already ingame. Room: " + roomID, new Exception("Cannot initialize ingame room. Room is already ingame"));
				return;
			}

			var room = RoomSystem.instance.FetchRoom (roomID);

			// Check if fetched room is null

			var info = new GameInfo (room.info.gameSettings, room.players);

			// Shuffle players sequence
			info.playerSequence = new Queue<PlayerInfo> (info.playerSequence.ToList().Shuffle ());

			// Initialize game values
			AssignShuffleCharacters (ref info);					// Assign characters to players
			CreateQuests (ref info);
			SetNewPartyLeader (roomID, StateTag.StartGame);

			gameInfoDictionary.Add (roomID, info);

			// Send information to everyone

			// Send information to each player their assigned characters
			// Send information with regards to their allies as well (if there are any with regards to their role)

			// Change state of game to PartySelection


			_logger.Info ("Created game for room " + roomID);
		}



		// player queue has been shuffled
		// Question is: Do we want to send an information that we haven't set the current leader
		// First sceneario: we could go ahead and send the information right away and adjust the sequencing in the clients' side
		// Second scenario: we can wait to send the information until we have sent the corrent information to the clients
		private void AssignShuffleCharacters (ref GameInfo info)
		{
			var characters = new List<int> (info.gameSettings.characters);

			// ### TODO
			// First get mobs for each faction 
			// Determine the balancing of characters according to player count

			// Assuming that's done, shuffle the characters and assign them to each player
			for (int i = 0; i < characters.Count (); i++)
				info.playerSequence.ElementAt(i).characterID = characters[i];

			_logger.Info ("Shuffled characters and assigned to Players");
		}

		private void CreateQuests (ref GameInfo info)
		{
			// Follow mechanics or guidelines in creating quests
			for (int i = 0; i < info.gameSettings.quests.Count (); i++)
				info.questTracker.Add (new QuestInfo (info.gameSettings.quests[i]));

			_logger.Info ("Created quests");
		}


		private void SetNewPartyLeader (ushort roomID, StateTag state)
		{
			// Fetch room gameinfo
			StateTag newState = StateTag.Game;
			var info = gameInfoDictionary[roomID];
			int currentLeaderID = -1;

			// Increment
			info.voteTrack++;

			// Check if request is for when the game just started 
			if (state.HasFlag (StateTag.StartGame))
			{
				currentLeaderID = SetNextLeader (ref info);
				newState |= StateTag.StartGame;
			}

			else if (state.HasFlag (StateTag.QuestCompletionDone))
			{
				currentLeaderID = SetNextLeader (ref info);
				// Send message first prior to proceeding
				// TODO : Create Message
				BroadcastMessageToPlayers (roomID, newState, null);

				// Proceed to PartySelection
				newState |= StateTag.PartySelection;
			}

			else if (state.HasFlag (StateTag.PartyApprovalTally))
			{
				// Approved Party Selection
				if (state.HasFlag (StateTag.Win))
				{
					// Reset vote tracker and reorder player sequence
					currentLeaderID = SetNextLeader (ref info);
					newState |= StateTag.QuestCompletion;   // Transition to Quest Completion Screen
															// Send message
				}

				// Disapproved Party Selection
				else if (state.HasFlag (StateTag.Loss))
				{
					
					if (info.voteTrack <= info.gameSettings.maxVotes)
					{
						currentLeaderID = info.playerSequence.ElementAt (info.voteTrack).ID;
						newState |= StateTag.QuestCompletion;
					}

					else
					{
						currentLeaderID = SetNextLeader (ref info);
						newState |= StateTag.QuestCompletionDone | StateTag.Loss;
					}
				}

				else
				{
					// Throw error if there are no results flagged in the state submitted
					_logger.Error ("Set new party leader failed",
						new Exception ("Requested to set new Party Leader for PartyApproval but there are no results flagged in state"));
				}
			}

			else
			{
				// throw error
				_logger.Error ("Set new party leader failed",
					new Exception ("Requested to set new PartyLeader but there are no categories flagged"));
			}

			// Reset reset values for all cases
			info.votes.Clear ();

			// Send message to clients that a new leader has been set
			// Send message to clients the new sequence as well for reference

			BroadcastMessageToPlayers (roomID, newState, null);
			_logger.Info ("Set new leader");
		}

		/// <summary>
		/// Set the next leader by reordering the sequence
		/// </summary>
		/// <param name="info">Game information that needs to be managed</param>
		/// <returns>
		/// Returns the client id of the current leader
		/// </returns>
		private int SetNextLeader (ref GameInfo info)
		{
			// Reset vote tracker and reorder player sequence
			info.voteTrack = 0;
			var previousLeader = info.playerSequence.Dequeue ();
			info.playerSequence.Enqueue (previousLeader);
			return info.playerSequence.Peek ().ID;
		}



		// The information that the clients will pass through the server and through this method
		// will be integers [-1, 0, 1]
		// -1 will represent the Quest failed
		// 0  will represent the Quest halted 
		// 1  will represent the Quest Success
		private void TallyVotes (ushort roomID, StateTag state, List<int> votes)
		{
			// Get the number of votes that are Greater the 1 (which represents Yes or Success)
			// Then, compare it to the number of votes with the value of Less than 1 (which represents No or Fail)
			int result = votes.Count (x => x > 0)
								.CompareTo (votes.Count (x => x < 0));

			// Check if the result is a tie
			if (result == 0)
			{
				// # NOT IMPORTANT
				// If the game is a tie, DO SOMETHING SPECIFIC FOR THIS MATTER

				state |= StateTag.Tie;
			}
			else
			{
				// Proceed as normal

				// State will be attached with the 
				state |= (result == 1 ? StateTag.Win : StateTag.Loss);

			}

			// TODO Value of the result will be submitted to the clients

			// Create a writer
			BroadcastMessageToPlayers (roomID, state, null);


			// Proceed to change the party leader
			SetNewPartyLeader (roomID, state);
		}

		// Send messages to all players in the rooom
		private void BroadcastMessageToPlayers (ushort roomID, StateTag state, DarkRiftWriter writer)
		{
			// Get room info
			var room = RoomSystem.instance.FetchRoom (roomID);

			foreach (var clientID in room.players)
			{
				// Fetch player's client
				var client = LobbySystem.FetchPlayerClient (clientID);
				// Send message
				using (Message message = Message.Create ((ushort) state, writer))
					client.SendMessage (message, SendMode.Reliable);
			}
		}

		// Filter as much information possible
		public void ReceiveMessage (StateTag state, Message message)
		{
			using (DarkRiftReader reader = message.GetReader ())
			{
				// Decrypt roomID from the message
				ushort roomID = reader.ReadUInt16 ();

				// Check if room actually exists in the room system

				// Check the room id and if it exists in the held disctionary
				if (!gameInfoDictionary.ContainsKey (roomID))
				{

					if (!state.HasFlag (StateTag.StartGame))
					{
						_logger.Error ("The room that you are trying to access does not exist. RoomID " + roomID, new Exception ());
						return;
					}

					// Create game
					InitializeGame (roomID);
					return;
				}

				// Other cases

				// RoomID does exist so check request

				// Tally Votes
				if (state.HasFlag (StateTag.PartyApprovalTally) || state.HasFlag (StateTag.QuestCompletionDone))
				{

					// Decrypt votes from the message provided/received
					var votes = new List<int> ();
					TallyVotes (roomID, state, votes);
					_logger.Info ("Tallying votes");
					return;
				}

				else
				{
					// throw error
				}
			}
		}


		class GameInfo
		{
			// First element in the queue sequence will be the leader for the turn
			// If the party that was formed by the current leader has been voted off 
			// We will use and integer or a value that will represent the VoteTrack 

			public GameSettingsInfo gameSettings;           // Current Settings of the game
			public Queue<PlayerInfo> playerSequence;         // This will be the order
			public List<QuestInfo> questTracker;			
			public List<ushort> currentPartyMembers;        // Members that are part of the party
			public List<int> votes;							// Current votes
			public int voteTrack;							// Track how many party formation has been voted off

			public GameInfo (GameSettingsInfo gameSettings, List<ushort> players)
			{
				this.gameSettings = gameSettings;
				playerSequence = new Queue<PlayerInfo> ();
				questTracker = new List<QuestInfo> ();
				currentPartyMembers = new List<ushort> ();
				votes = new List<int> ();
				voteTrack = 0;

				// Create PlayerInfo for each player
				foreach (var playerID in players)
					playerSequence.Enqueue (new PlayerInfo (playerID));
			}

		}

		class QuestInfo 
		{
			public ushort partyMembers;						// Number of party members to go on a quest
			public int result;                              // Result of the Quest

			public QuestInfo (ushort partyMembers)
			{
				this.partyMembers = partyMembers;
			}
		}

		class PlayerInfo
		{
			public readonly ushort ID;
			public int characterID;

			public PlayerInfo (ushort playerID, int characterID = -1)
			{
				ID = playerID;
				this.characterID = characterID;
			}
		}
	}
}
