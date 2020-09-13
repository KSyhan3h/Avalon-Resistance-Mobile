using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using AvalonResistance.States;
using AvalonResistance.Utilities;
using DarkRift.Client;


// This class will be handling basic networking as well
// This will also handle simple data send/receive functions
namespace AvalonResistance
{
    public class GameMaster : MonoBehaviour
    {
        public int questNum;
        public int voteTrack;

        // Sequence
        [SerializeField] private List<Player> players;
        [SerializeField] private GameSettings gameSettings;
        
        [SerializeField] private List<Faction> factions;
        [SerializeField] private List<Character> characters;

        private StateTag currentPhase;
        private int leaderSequenceCount;        // Tracks the current leader in the sequence
        private int disbandPartyCount;          // Denied and disbanded parties counter

        public void Start ()
        {
        }

        public void Reset () 
        {
            // Clear Previous State and Remove any unneeded items on the scene
            currentPhase = StateTag.RoleAssignment;
            factions = null;
            characters = null;
            disbandPartyCount = 0;
        }

        private void Initialize () 
        {
            // Randomize Player Turns
            players.Shuffle ();

            // Get All Factions
            factions = new List<Faction> (gameSettings.factions);
            
            // Get All Characters
            if (characters == null)
            {
                characters = new List<Character> ();
                foreach (var faction in factions)
                    characters.AddRange (faction.characters.ToList ());

                for (int i = players.Count - characters.Count; i > 0; i--)
                    characters.Add (gameSettings.g_mob);
            }

            // Shuffle characters sequence
            characters.Shuffle ();

            // Assign Role to Player
            for (int i = 0; i < players.Count; i++)
                players[i].character = characters[i];

            // Trigger Phase for animation and stuff
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