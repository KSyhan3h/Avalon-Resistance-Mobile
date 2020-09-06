using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateQuestParty : MonoBehaviour
{
    public bool currentLeader;



    #region Leader Role
    public void SelectMember (Player player)
    {
    }

    public void RemoveMember (Player player)
    {
    } 

    public void SubmitPartyCreation ()
    {
    }
    #endregion

    #region Non-leader Role
    public void ReceivePartyCreation ()
    {
    }

    public void SelectVote (PartyVote vote)
    {
    }

    public void SubmitVote () 
    {
    }
    #endregion
}
