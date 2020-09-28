using UnityEngine;
using DarkRift.Client;
using AvalonResistance.States;
using TMPro;

namespace AvalonResistance 
{
    public class LobbyManager : MonoBehaviour
    {
		#region UI
        #endregion


		private void Start ()
        {
            // Set events 
        }

        public void RequestUpdateLobby ()
        {

        }

        private void UpdateLobby ()
        {
            // Update the whole lobby
        }

        // Add a room to list of rooms in the lobby
        private void AddRoom ()
        {

        }

        // Remove a room from the list in the lobby
        private void RemoveRoom ()
        {

        }

        public void CreateRoom ()
        {
            // Send message to Server that player wants to create a room and designate this player as the host
        }

        public void JoinRoom ()
        {
            // Send message to server to join room
        }

        public void ReceiveMessage (StateTag stateTag, MessageReceivedEventArgs e)
        {
            // Check if stateTag has Settings Tag on it
            if (!stateTag.HasFlag (StateTag.Lobby))
                return;


        }

        private class LobbyRoom
        {
            public RoomInfo roomInfo;
            public TMP_Text roomID;
            public TMP_Text roomName;
        }
    }
}