using UnityEngine;
using UnityEngine.InputSystem;

// Manages Player Spawning and Joining
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public bool AllPlayersPresent { get; private set; }                                       // Checks if All Players are Now Present
    public bool IsAllSet { get; set; }                                                        // Checks if All Players are All Set

    // Private Variables
    private PlayerInputManager playerInputManager;                                            // PlayerInputManager Component Reference
    
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

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Variables
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = GameManager.Instance.PlayerPrefabs[0];
        AllPlayersPresent = false;
        IsAllSet = false;
    }

    #region Player Events
    // On Player Joined Event
    public void Join()
    {
        // Execute as long as not all players are present
        if (playerInputManager.playerCount != playerInputManager.maxPlayerCount)
        {
            // Change Input Manager Prefab
            playerInputManager.playerPrefab = GameManager.Instance.PlayerPrefabs[1];

            // Indicate 1st Player as Ready
            UIController.Instance.PlayerIsReady(playerInputManager.playerCount - 1);

            // Play Ready SFX
            AudioManager.Instance.PlayOneShot("ready");
        }
        // Disable Joining if Maximum Players have been Reached
        else
        {
            playerInputManager.DisableJoining();

            // Initiate Game Countdown
            GameManager.Instance.SetGameState(GameManager.GameStates.COUNTDOWN);
            EventManager.Instance.MatchStart.Invoke();

            // Indicate 2nd Player as Ready
            // Call Only Once
            if (!AllPlayersPresent)
            {
                UIController.Instance.PlayerIsReady(playerInputManager.playerCount - 1);

                // Play Ready SFX
                AudioManager.Instance.PlayOneShot("ready");
            }

            // Declare all players present
            AllPlayersPresent = true;
            IsAllSet = true;
        }
    }
    #endregion
}
