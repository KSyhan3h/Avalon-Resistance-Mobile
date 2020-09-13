using System;
using DarkRift.Server;
using AvalonServerPlugin.Scripts.Networking;
using AvalonServerPlugin.Scripts.System;

namespace AvalonServerPlugin
{
	public class AvalonServerPlugin : Plugin
	{
		public static AvalonServerPlugin instance { get; private set; }

		public override bool ThreadSafe => false;
		public override Version Version => new Version (0, 0, 1);

		public LobbySystem _lobbySystem;
		public PlayerSystem _playerSystem;
		public RoomSystem _roomSystem;

		public AvalonServerPlugin (PluginLoadData pluginLoadData) : base (pluginLoadData)
		{
			if (instance != null)
			{
				Logger.Error ("There is an existing instance of AvalonServerPlugin", new Exception ("Duplicate instance of AvalonServerPlugin"));
				return;
			}
			instance = this;

			// Set Events
			ClientManager.ClientConnected += OnClientConnected;
			ClientManager.ClientDisconnected += OnClientDisconnected;

			// Instantiate Logic Systems
			_lobbySystem = new LobbySystem (this.Logger);
			_playerSystem = new PlayerSystem (this.Logger);
			_roomSystem = new RoomSystem (this.Logger);
		}

		#region Connection Status
		private void OnClientConnected (object sender, ClientConnectedEventArgs e)
		{
			Logger.Info ("Client " + e.Client.ID + " has joined the server");

			// # Set Events
			// Listen to client
			e.Client.MessageReceived += OnClientReceivedMessage;

			// Add player to lobby
			_lobbySystem.AddPlayer (e.Client);
		}

		private void OnClientDisconnected (object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Info ("Client " + e.Client.ID + " has left the server");
			// Remove player from the player pool
			_lobbySystem.RemovePlayer (e.Client);
		}
		#endregion

		private void OnClientReceivedMessage (object sender, MessageReceivedEventArgs e)
		{
			Logger.Info ("Received message from: " + e.Client.ID + "\n\nMessage: ");

			// Check tag
			switch ((ClientMessageTag) e.Tag)
			{
				case ClientMessageTag.Player:
					_playerSystem.ReceiveMessage (e.Client, e.GetMessage ());
					break;

				case ClientMessageTag.Lobby:
					_lobbySystem.ReceiveMessage (e.Client, e.GetMessage ());
					break;

				case ClientMessageTag.Room:
					break;

				case ClientMessageTag.Game:
					break;

				case ClientMessageTag.Chat:
					break;
			}
		}
	}
}
