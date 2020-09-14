using System;
using System.Collections.Generic;
using System.Linq;
using DarkRift.Server;
using AvalonServerPlugin.Scripts.Models;
using AvalonServerPlugin.Scripts.Models.Info;
using DarkRift;

namespace AvalonServerPlugin.Scripts.System
{
	/// <summary>
	/// Handles logic with regards to updating information within the room
	/// Information like gamesettings, room name
	/// </summary>
	class RoomSystem
	{
		public static RoomSystem instance { get; private set; }

		private Logger _logger;

		private List<RoomModel> _rooms;


		public RoomSystem (Logger logger)
		{
			if (instance != null)
			{
				logger.Error ("There is an existing instance of RoomSystem", new Exception ("Duplicate instance of RoomSystem"));
				return;
			}
			instance = this;

			_logger = logger;
			_rooms = new List<RoomModel> ();
		}

		public void CreateRoom (IClient client, RoomInfo roomInfo)
		{
			// Client has hosted the room

			// Check if there is a vacant room that can be used 
			var room = FetchEmptyRoom ();

			// If there are avaiable rooms, use it 
			if (room != null)
			{
				// USE ROOM
				// UPDATE ROOM STATE
				room.state = RoomState.Open;
				// UPDATE THE ROOM MODEL INFO
				room.info = roomInfo;
			}
			else
			{
				// Craete new room 
				room = new RoomModel (roomInfo);
				// Add to list of rooms
				_rooms.Add (room);
			}

			// After selecting a room, add the host to the room automatically 

			// Set the host as the leader of the room
			room.hostID = client.ID;

			// Information to pass to Everyone - roommodel info
		}

		public void AddPlayerToRoom (IClient client, ushort roomID)
		{
			// Check if the room exists
			var room = FetchRoom (roomID);

			// If room exists
			if (room == null)
			{
				// throw error
				throw new Exception ("Room does not exists");
			}

			// Check if player exists in the room already
			if (room.players.Contains (client.ID))
			{
				// If player already exists
				// throw error
				throw new Exception ("Player already exists in the room");
			}

			// Register player into the room
			room.players.Add (client.ID);

			// Fetch player from LobbySystem
			var player = LobbySystem.FetchPlayer (client);
			// Change playerstatus
			player.roomID = roomID;

			_logger.Info (string.Format ("Client {0} is now part of room #{1}", client.ID, roomID));

			// TODO : Send message to the client to enter the room OR to display the room UI 
			// provide the room information that the player needs to enter
		}

		public void RemovePlayerFromRoom (IClient client, ushort roomID)
		{
			// Check if the room exists
			var room = FetchRoom (roomID);

			// If room exists
			if (room == null)
			{
				// throw error
				throw new Exception ("Room does not exists");
			}

			// Check if player exists in the room already
			if (!room.players.Contains (client.ID))
			{
				// If player already exists
				// throw error
				throw new Exception ("Player does not exist in the room");
			}

			var player = LobbySystem.FetchPlayer (client);
			player.roomID = PlayerModel.NO_ROOM;

			_logger.Info (string.Format ("Client {0} is no longer part of room #{1}", client.ID, roomID));

			// Send message to everyone in the room that the player has been removed/left the room; 
			// If Lobby needs the information that the a player has been added to the idle players in the lobby,
			// that is if the player is still connected to the server, provide the information as well 
		}

		public void UpdateRoomInfo ()
		{ 
		}

		public RoomModel FetchRoom (ushort roomID)
		{
			return _rooms.Find (x => x.ID == roomID);
		}

		public RoomModel FetchEmptyRoom ()
		{
			return _rooms.FirstOrDefault (x => x.state == RoomState.Open);
		}

		#region Network Communication
		public void SendMessage ()
		{
		
		}

		public void ReceiveMessage (IClient client, Message message)
		{
			_logger.Info ("RoomSystem received the message: " + message.ToString ());
		}
		#endregion
	}
}
