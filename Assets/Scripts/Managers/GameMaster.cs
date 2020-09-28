using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using AvalonResistance.States;
using AvalonResistance.Utilities;
using DarkRift.Client;
using DarkRift;


// This class will be handling basic networking as well
// This will also handle simple data send/receive functions
// This class will communicate with BoardManager to set up the player's ui

namespace AvalonResistance
{
    [DisallowMultipleComponent]
    public class GameMaster : MonoBehaviour
    {
        #region UI Components

        #endregion

        [SerializeField] private Player player;                     // local player
		[SerializeField] private List<Player> players;              // sequence of players
        [SerializeField] private GameSettings gameSettings;         // game settings
        
        [SerializeField] private List<Faction> factions;
        [SerializeField] private List<Character> characters;

        public Player CurrentLeader { get { return players[revoteCount]; } }

        private StateTag currentState;                              // Will keep track on the current phase or state of the game
        private int revoteCount;                                    // Tracks the current leader in the sequence

        public void Start ()
        {
        }

        private void InitializeGame (Message message)
        { 
            // Will get the list of players with their roles
            // Receive Quest info
            // Set new party leader 
            // Set new state
        }

        private void PartySelection (StateTag stateTag, Message message)
        {
            // Received update on the new selections in the Party
            if (stateTag.HasFlag (StateTag.PresentPartySelection))
            { 
            
            }

            // Show results of the Party Selection
            else if (stateTag.HasFlag (StateTag.PartyApprovalTally))
            {

            }

            else
            {
                // Initialize the PartySelection
            }
        }

        private void QuestCompletion (StateTag stateTag, Message message)
        { 
        
        }

        private void SetCurrentLeader ()
        { 
        
        }

        private void EndGame ()
        { 
        
        }

        private void ShowGameResult ()
        { 
        
        }

        /// <summary>
        /// Submit party votes or quest completion
        /// </summary>
        /// <param name="voteTag">
        /// Values to choose would be Win and Lose
        /// Win can stand-in as Approve while Lose will stand-in as Disapprove
        /// </param>
        public void SubmitVote (StateTag voteTag)
        { 
          
        }

        public void ReceiveMessage (StateTag stateTag, MessageReceivedEventArgs e)
        {
            // Check if stateTag has Game Tag on it
            if (!stateTag.HasFlag (StateTag.Game))
                return;

            if (stateTag.HasFlag (StateTag.StartGame))
            {
                InitializeGame (e.GetMessage ());
                return;
            }

            // First we set the role of the player provided by the server
            // We set the sequence of the players (not necessary for now but useful)
            // We set the 

            else if (stateTag.HasFlag (StateTag.PartySelection))
            {
                PartySelection (stateTag, e.GetMessage ());
                return;
            }

            else if (stateTag.HasFlag (StateTag.QuestCompletion))
            {
                QuestCompletion (stateTag, e.GetMessage ());
                return;
            }

            else if (stateTag.HasFlag (StateTag.EndGame))
            {
                EndGame ();
                return;
            }

        }

    }
}