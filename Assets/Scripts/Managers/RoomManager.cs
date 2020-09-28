using UnityEngine;
using DarkRift;
using AvalonResistance.States;

// This will handle the information and the UI about the Room that the player is currently in

namespace AvalonResistance 
{
	public class RoomManager : MonoBehaviour
	{
		// Host 
		// Members
		[SerializeField] private Transform playersHolder;


		// Sets the initial information about the room
		private void InitializeRoom ()
		{ 
			// Get and set the information about room info (game settings and stuff)
			// Call UpdateRoomInfo 
			
			// Get info about players in the room already
			// Loop through them and call AddPlayer function to add these players
		}

		// Updates the Room information
		private void UpdateRoomInfo ()
		{ 
		
		}

		// Adds a new player into the room
		private void AddPlayer ()
		{ 
		
		}

		// Removes an existing player from the room
		private void RemovePlayer ()
		{ 
		
		}

		// Hose will force start the game
		public void StartGame ()
		{ 
			
		}

		public void ReceiveMessage (StateTag stateTag, Message message)
		{
			// If message received is not for 
			if (!stateTag.HasFlag (StateTag.Room))
				return;


		}
	}
}