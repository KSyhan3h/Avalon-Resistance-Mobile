using System;
using DarkRift;
using DarkRift.Server;

namespace AvalonServerPlugin.Scripts.System
{
	/// <summary>
	/// Handles logic and data handling related to player info
	/// </summary>
	class PlayerSystem
	{
		public static PlayerSystem instance { get; private set; }

		private Logger _logger;

		public PlayerSystem (Logger logger)
		{
			if (instance != null)
			{
				logger.Error ("There is an existing instance of RoomSystem", new Exception ("Duplicate instance of RoomSystem"));
				return;
			}
			instance = this;

			_logger = logger;
		}

		#region Private Methods
		private void UpdatePlayerInfo (IClient client, PlayerInfo playerInfo)
		{
			// Fetch PlayerModel from LobbySystem
			LobbySystem.FetchPlayer (client).info = playerInfo;

			// Send message to everyone that needs the information
		}
		#endregion

		#region Public Methods
		public void SendMessage ()
		{ 
		
		}

		public void ReceiveMessage (IClient client, Message message)
		{
			_logger.Info ("PlayerSystem received the message: " + message.ToString ());
		}
		#endregion
	}
}
