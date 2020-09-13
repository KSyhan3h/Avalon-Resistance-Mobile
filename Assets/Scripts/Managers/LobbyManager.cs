using UnityEngine;
using DarkRift.Client;
using AvalonResistance.States;


namespace AvalonResistance 
{
	public class LobbyManager : MonoBehaviour 
	{
        private void AddLobby ()
        { 
        
        }

        private void SendMessage ()
        {

        }

        private void ReceivedMessage (StateTag stateTag, MessageReceivedEventArgs e)
        {
            // Check if stateTag has Settings Tag on it
            if (!stateTag.HasFlag (StateTag.Lobby))
                return;


        }
    }
}