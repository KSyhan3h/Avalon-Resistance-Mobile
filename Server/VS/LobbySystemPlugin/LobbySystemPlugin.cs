using System;
using DarkRift.Server;


namespace LobbySystemPlugin
{
	public class LobbySystemPlugin : Plugin
	{
		public override bool ThreadSafe => throw new NotImplementedException ();
		public override Version Version => throw new NotImplementedException ();
		
		// Constructor
		public LobbySystemPlugin (PluginLoadData pluginLoadData) : base (pluginLoadData)
		{
			
		}

		private void OnClientConnected (object sender, ClientConnectedEventArgs e)
		{
			Logger.Info ("Client " + e.Client.ID + " has joined the server");

			// # Set Events
			// Listen to client
			e.Client.MessageReceived += Client_MessageReceived;

			// Add player to lobby
			_lobbySystem.AddPlayer (e.Client);
		}

		private void OnClientDisconnected (object sender, ClientDisconnectedEventArgs e)
		{
			Logger.Info ("Client " + e.Client.ID + " has left the server");
			// Remove player from the player pool
		}

		private void Client_MessageReceived (object sender, MessageReceivedEventArgs e)
		{
			Logger.Info ("Received message from: " + e.Client.ID + "\n\nMessage: ");

			// Check tag
			switch ((ClientState) e.Tag)
			{
				case ClientState.Player:
					break;

				case ClientState.Lobby:
					_lobbySystem.ReceiveMessage (e);
					break;
			}
		}
	}
}
