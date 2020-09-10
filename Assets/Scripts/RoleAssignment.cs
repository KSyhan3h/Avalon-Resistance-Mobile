using System.Collections.Generic;
using UnityEngine;

/// <summary> Handles the UI for Role Assignment and Player Sequence at the beginning of the round </summary>
public class RoleAssignemnt : MonoBehaviour
{
	private List<Player> _players;

	// Receive List of players
	public void ReceivePlayers (List<Player> players) 
	{
		_players = players;
	}

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
}