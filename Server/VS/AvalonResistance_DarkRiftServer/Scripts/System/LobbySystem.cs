using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvalonServerPlugin.Scripts.Models;
using AvalonServerPlugin.Scripts.Networking;
using DarkRift;
using DarkRift.Server;

namespace AvalonServerPlugin.Scripts.System
{
	/// <summary>
	/// This will handle the logic for the lobby
	/// </summary>
	public class LobbySystem
	{
		public static LobbySystem instance { get; private set; }

		private Logger _logger;

		private List<PlayerModel> _players;
		private RoomSystem _roomSystem { get { return RoomSystem.instance; } }

		// Constructor
		public LobbySystem (Logger logger)
		{
			// Throw error if instance is null 
			if (instance != null)
			{
				logger.Error ("There is an existing instance of LobbySystem", new Exception ("Duplicate instance of LobbySystem"));
				return;
			}
			instance = this;


			_logger = logger;
			_players = new List<PlayerModel> ();
		}

		public void UpdateLobbyInformation (IClient client)
		{
			//ConsoleLog ("Client " + clientID + " has requested to update their lobby", ConsoleColor.Cyan);
			_logger.Info ("Client " + client.ID + " has requested to update their lobby");
		}

		public void AddPlayer (IClient client)
		{
			// Check if player is already in the pool
			if (_players.Find (x => x.client == client) != null)
			{
				// throw error that player is already in the pool
				_logger.Error ("Player is already exists in the player pool");
			}
			
			// Player is not included in the pool so add the player
			_players.Add (new PlayerModel (client));
			_logger.Info ("Client " + client.ID + " has been added to the player pool");
			UpdateLobbyInformation (client);
		}

		public void RemovePlayer (IClient client)
		{
			// Check if the player is in the pool
			var player = _players.Find (x => x.client == client);

			if (player == null)
			{
				_logger.Error ("Client cannot be removed because it does not exist in the pool.");
			}
			else
			{
				_players.Remove (player);
				_logger.Info ("Client has been removed from the pool");
			}
		}

		public PlayerModel FetchPlayer (IClient client)
		{
			return _players.Find (x => x.client == client);
		}

		public void SendMessage (IClient client, Message message)
		{
			// # Update Client 
			// When updating the client we can use the Unreliable Sending method
		}

		// Parse the message received and decode it to handle the request or message
		public void ReceiveMessage (IClient client, Message message)
		{
			_logger.Info ("LobbySystem received the message: " + message.ToString ());
		}
	}
}
