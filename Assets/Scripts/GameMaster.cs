using System.Collections.Generic;
using UnityEngine;

public enum Phases
{
    RoleAssignment,
    PartySelection,
    QuestResult,
    Assassination
}

public class GameMaster : MonoBehaviour
{
    public int questNum;
    public int voteTrack;

    // Sequence
    public Faction[] factions;
    public Player[] players;

    public void Start () 
    {
        Test ();
    }

    public void Test ()
    {
        
    }

}
