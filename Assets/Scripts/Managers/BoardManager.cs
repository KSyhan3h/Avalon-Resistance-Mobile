using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class will handle all UI releated information and process In-Game
/// </summary>

namespace AvalonResistance
{
    public class BoardManager : MonoBehaviour
    {
        #region MainBoard UI
        // Player
        [SerializeField] private Image playerPortrait;
        [SerializeField] private TMP_Text playerName;
        [SerializeField] private Image characterPortait;
        [SerializeField] private Image alliancePortrait;

        // Information
        [SerializeField] private Transform informationList;

        // Quest
        //[SerializeField]
        #endregion

        #region MainBoard
        // Update Player Name & Character
        private void InitializeBoardUI ()
        {



        }


        private void UpdateQuest ()
        {
            // Set quest information
        }

        private void UpdatePlayer ()
        {
            // We need the player name & profile
            // We need the character name and UI
        }

        private void UpdateInformationList ()
        {
            // Set known information

        }
        #endregion

        #region PartyFormation 
        private void SetNewLeader ()
        {
            // Set new leader 
        }

        private void UpdatePartyFormation ()
        {
            // Check if you are the current party leader
        }

        private void AddVoteTrack ()
        {
            // Adds +1 to vote track
        }

        private void ResetVoteTrack ()
        {

        }
        #endregion

        #region QuestCompletion

        #endregion
    }
}