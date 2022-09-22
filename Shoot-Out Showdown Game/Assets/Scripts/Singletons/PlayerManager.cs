using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public bool AllPlayersPresent { get; private set; }                                             // Checks is all players are now present

    private PlayerInputManager playerInputManager;

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = GameManager.Instance.PlayerPrefabs[0];
        AllPlayersPresent = false;
    }
    #endregion

    // On Player Joined Event
    public void Join()
    {
        // Execute as long as not all players are present
        if (playerInputManager.playerCount != playerInputManager.maxPlayerCount)
        {
            // Change Input Manager Prefab
            playerInputManager.playerPrefab = GameManager.Instance.PlayerPrefabs[1];
        }
        // Disable Joining if Maximum Players have been Reached
        else
        {
            playerInputManager.DisableJoining();
            GameManager.Instance.SetGameState(GameManager.GameStates.COUNTDOWN);

            EventManager.Instance.MatchStart.Invoke();

            AllPlayersPresent = true;
        }
    }
}
