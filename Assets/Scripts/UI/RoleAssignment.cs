using AvalonResistance.States;

using DarkRift.Client;

using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Handles the UI for Role Assignment and Player Sequence at the beginning of the round 
/// </summary>

namespace AvalonResistance
{
	public class RoleAssignemnt : MonoBehaviour
	{
		// Get prefab to instaniate and show player information 


		// #region UI ELEMENTTS

		public void DisplayPlayerRole ()
		{
			// Show the Playe's Role
			// Play animation 
		}

		public void ShowInitialInformation ()
		{
			// Show initial information based on player's identity and traits
			// This will provide who are player's evil allies, Merlin w/o Morgana, 
			// or blind information as a knight
		}

		private void SendMessage ()
		{

		}

		private void ReceivedMessage (StateTag stateTag, MessageReceivedEventArgs e)
		{
			// Check if stateTag has Settings Tag on it
			if (!stateTag.HasFlag (StateTag.Game))
				return;
		}
	}
}