using System;
using UnityEngine;
using AvalonResistance.Data;
using AvalonResistance.States;
using DarkRift.Server;

/// <summary>
/// Handles setting and overall manager
/// </summary>
namespace AvalonResistance
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }


        [SerializeField] private NetworkManager _networkManager;
        [SerializeField] private LobbyManager _lobbyManager;
        [SerializeField] private RoomManager _roomManager;


        [SerializeField] private Settings _defaultSettings;
        [SerializeField] private Settings _currentSettings;


        public Action OnQuitGame;


        private void Awake ()
        {
            if (instance != null)
            {
                Destroy (this.gameObject);
                return;
            } 

            instance = this;
            DontDestroyOnLoad (this.gameObject);
        }

        private void Start ()
        {
            // Throw exceptions if manager is not initialized
            #region Managers
            if (!_networkManager)
                ThrowExceptionIfNull (_networkManager);

            // if (!_lobbyManager)
            //     ThrowExceptionIfNull (_lobbyManager);
            #endregion
        }
        

        private void ThrowExceptionIfNull<T> (T obj)
        {
            if (obj == null)
                Debug.LogError ("No instance of " + typeof (T));
        }

        private void OnApplicationQuit ()
        {
            OnQuitGame?.Invoke ();
        }

        private void SendMessage ()
        { 
            
        }

        private void ReceivedMessage (StateTag stateTag, MessageReceivedEventArgs e)
        {
            // Check if stateTag has Settings Tag on it
            if (!stateTag.HasFlag (StateTag.Settings))
                return;


        }
    }
}

