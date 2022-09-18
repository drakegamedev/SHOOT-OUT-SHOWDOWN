using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        SETTING_UP,
        COUNTDOWN,
        ROUND_START,
        ROUND_OVER
    };
    
    public static GameManager Instance;

    [Header("References")]
    public GameObject Camera;                                                                       // Camera GameObject Reference
    public PlayerInputManager PlayerInputManager;                                                   // Player Input Manager Reference
    public Color[] ArenaColors;                                                                     // Arena Color
    public GameObject[] PlayerPrefabs;                                                              // Player Prefabs Array
    public string[] ArenaIds;                                                                       // Arena ID's
    public GameStates CurrentGameState;                                                             // Current Game State
    public UIController UiController { get; set; }                                                  // UI Controller Reference
    public Camera GameCamera { get; private set; }                                                  // Camera Component Reference
    public CameraController CameraController { get; private set; }                                  // Camera Controller Class Reference

    // Lists
    public List<GameObject> PlayerList { get; set; } = new();                                       // Player List
    public List<Transform> PlayerSpawnPoints { get; set; } = new();                                 // Player Spawn Points

    public bool allPlayersPresent { get; private set; }                                             // Checks is all players are now present

    // Private Variables
    private string currentSceneId;
    private string currentArenaId;

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

    void OnDisable()
    {
        EventManager.Instance.ResetMatch -= ReactivatePlayers;
        EventManager.Instance.ResetMatch -= GenerateArena;

        SceneManager.UnloadSceneAsync(currentArenaId);
        SceneManager.UnloadSceneAsync("UIScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Variables
        CurrentGameState = GameStates.ROUND_START;
        GameCamera = Camera.GetComponent<Camera>();
        CameraController = Camera.GetComponent<CameraController>();
        PlayerInputManager.playerPrefab = PlayerPrefabs[0];
        allPlayersPresent = false;

        // Add UI Scene
        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);

        EventManager.Instance.ResetMatch += ReactivatePlayers;
        EventManager.Instance.ResetMatch += GenerateArena;

        // Initial Genration of Arena
        GenerateArena();
    }

    // On Player Joined Event
    public void Join()
    {
        // Disable Joining if Maximum Players have been Reached
        if (PlayerInputManager.playerCount == PlayerInputManager.maxPlayerCount)
        {
            PlayerInputManager.DisableJoining();
            allPlayersPresent = true;
        }
        else
        {
            // Change Input Manager Prefab
            PlayerInputManager.playerPrefab = PlayerPrefabs[1];
        }
    }

    public void SetGame()
    {
        CurrentGameState = GameStates.ROUND_OVER;
        CameraController.CameraShake();
        EventManager.Instance.PlayerDied.Invoke();
    }

    
    // Reset Player Properties
    public void ReactivatePlayers()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            // Reactivate Player Prefab
            PlayerList[i].SetActive(true);

            // Reset Player Health
            PlayerHealth playerHealth = PlayerList[i].GetComponent<PlayerHealth>();
            playerHealth.ResetHealth();

            // Reset Player Ammo
            PlayerSetup playerSetup = PlayerList[i].GetComponent<PlayerSetup>();
            playerSetup.Gun.ResetAmmo();
        }
    }

    #region Level Generation
    public void GenerateArena()
    {
        int randomArena = UnityEngine.Random.Range(0, ArenaIds.Length);

        while (ArenaIds[randomArena] == currentArenaId)
        {
            randomArena = UnityEngine.Random.Range(0, ArenaIds.Length);
        }
        
        currentArenaId = ArenaIds[randomArena];

        Debug.Log(currentArenaId);

        StartCoroutine(InitializeArena(ArenaIds[randomArena]));
    }

    IEnumerator InitializeArena(string arenaId)
    {
        if (currentSceneId != null)
        {
            yield return SceneManager.UnloadSceneAsync(currentSceneId);
        }

        Resources.UnloadUnusedAssets();
        yield return null;
        GC.Collect();
        yield return null;

        yield return SceneManager.LoadSceneAsync(arenaId, LoadSceneMode.Additive);

        currentSceneId = arenaId;
    }
    #endregion
}
