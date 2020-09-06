using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    RoleAssignment,
    SelectPartyMembers,
    ApprovePartyMembers,
    CompleteQuest,
    Assassination
}

// This class will be under NetworkBehaviour class
// This will also handle simple data send/receive functions

public class GameMaster : MonoBehaviour
{
    public GameObject cardModel;
    public Sprite backfaceImage;

    public int questNum;
    public int voteTrack;

    // Sequence
    public List<Player> players;
    public GameSettings gameSettings;

    private List<Faction> factions;
    private List<Character> characters;

    private GamePhase currentPhase;
    private int leaderSequenceCount;        // Tracks the current leader in the sequence
    private int disbandPartyCount;          // Denied and disbanded parties counter

    public void Start () 
    {
        Reset ();
        Initialize ();
    }

    public void Reset () 
    {
        // Clear Previous State and Remove any unneeded items on the scene
        currentPhase = GamePhase.RoleAssignment;
        factions = null;
        characters = null;
        disbandPartyCount = 0;
    }

    public void Initialize () 
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

    public void SetNewPartyLeader (bool nextSequence) 
    {
        if (nextSequence)
        {
            disbandPartyCount = 0;
        }
    }

    public void SelectPartyMembers ()
    {
        // Set Party Leader
        leaderSequenceCount = 0;
        

        currentPhase = GamePhase.SelectPartyMembers;
    }

    public void ReceiveApprovalVote ()
    {

    }

    public void ShowVoteTally () 
    {
        // Tally and Show results


        // If party members proceeds to the quest
        disbandPartyCount = 0;


        // If party members are disbanded
        disbandPartyCount++;
    }

    public void CompleteQuest () 
    {
        currentPhase = GamePhase.CompleteQuest;
    }

    public void ReceiveQuestResult () 
    {
    }

    public void ShowQuestResult () 
    {
        // Show Tally and Results of the Quest Completion
    }


    public void ShowGameResult () 
    {

    }
}
