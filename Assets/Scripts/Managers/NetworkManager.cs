using System;
using System.Net;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using AvalonResistance.States;
using UnityEditor.U2D;

namespace AvalonResistance
{
	[DisallowMultipleComponent]
	public class NetworkManager : MonoBehaviour
	{
		private DarkRiftClient _client;

		[SerializeField] private string _ipAddress;
		[SerializeField] private int _port;
		[SerializeField] private string testMessage;

		public Action<StateTag, MessageReceivedEventArgs> OnClientReceivedMessage;
		
		public ConnectionState GetClientState { get { return _client.ConnectionState; } }
		public bool IsClientConnectedToServer { get { return _client.ConnectionState == ConnectionState.Connected; } }

		private void Start () 
		{
			// Initialize values
			_client = new DarkRiftClient ();

			// Set Actions and Events
			_client.MessageReceived += ClientReceivedMessage;
			GameManager.instance.OnQuitGame += DisconnectFromServer;
		}

		public void ConnectToServer ()
		{
			if (IsClientConnectedToServer)
			{
				Debug.LogWarning ("Client has already connected to the server");
				return;
			}

			if (GetClientState == ConnectionState.Connecting)
			{
				Debug.LogWarning ("Client is already trying to connect");
				return; 
			}

			// Attempt to connect to server 
			try 
			{
				_client.Connect (IPAddress.Parse (_ipAddress), _port, false);
				Debug.Log ("Client has successfully connected to the server");
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public void DisconnectFromServer ()
		{
			if (IsClientConnectedToServer)
				Debug.Log ("Client is disconnecting from the server");
			_client.Disconnect ();
		}

		public void SendMessageToServer (StateTag tag, Message message, SendMode sendMode)
		{
			if (_client == null)
			{
				Debug.LogError ("There is no client assigned");
				return;
			}

			_client.SendMessage (message, SendMode.Reliable);
			Debug.Log ("Client as has sent the message");
		}

		public void ClientReceivedMessage (object sender, MessageReceivedEventArgs e) 
		{
			// Check for Tags first
			StateTag state = (StateTag) e.Tag;

			if (state.HasFlag (StateTag.Network))
			{
				
				return;
			}

			OnClientReceivedMessage?.Invoke (state, e);
		}
	}
}