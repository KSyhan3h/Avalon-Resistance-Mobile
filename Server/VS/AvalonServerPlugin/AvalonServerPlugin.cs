using System;
using DarkRift.Server;
using AvalonServerPlugin.Scripts;
using AvalonServerPlugin.Scripts.System;

namespace AvalonServerPlugin
{
	class AvalonServerPlugin : Plugin
	{
		public static AvalonServerPlugin instance { get; private set; }

		public override bool ThreadSafe => false;
		public override Version Version => new Version (0, 0, 1);

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

			// Initialize Systems
			new GameSystem (Logger);
			new LobbySystem (Logger);
			new RoomSystem (Logger);
		}

		public Logger GetLogger ()
		{
			return Logger;
		}

		#region Connection Status
		private void OnClientConnected (object sender, ClientConnectedEventArgs e)
		{
			Logger.Info ("Client " + e.Client.ID + " has joined the server");

			// # Set Events
			// Listen to client
			e.Client.MessageReceived += OnClientReceivedMessage;

			// Add player to lobby
			LobbySystem.instance.AddPlayer (e.Client);
		}

		private void OnClientDisconnected (object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Info ("Client " + e.Client.ID + " has left the server");
			// Remove player from the player pool
			LobbySystem.instance.RemovePlayer (e.Client);
		}
		#endregion

		private void OnClientReceivedMessage (object sender, MessageReceivedEventArgs e)
		{
			Logger.Info ("Received message from: " + e.Client.ID + "\n\nMessage: ");

			// Check tag
			var state = (StateTag) e.Tag;

			if (state.HasFlag (StateTag.Game))
			{
				// Pass to gaem system
				GameSystem.instance.ReceiveMessage (state, e.GetMessage ());
			}

			else if (state.HasFlag (StateTag.Lobby))
			{ 
				// Pass to lobbysystem
				//LobbySystem.instance.Re
			}
		}
	}
}
