using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Color[] ArenaColors;                                                                     // Arena Color
    public GameObject[] PlayerPrefabs;                                                              // Player Prefabs Array
    public string[] ArenaIds;                                                                       // Arena ID's
    public GameStates CurrentGameState;                                                             // Current Game State
    public Camera GameCamera { get; private set; }                                                  // Camera Component Reference
    public CameraController CameraController { get; private set; }                                  // Camera Controller Class Reference
    public int CurrentArenaColorIndex { get; set; }                                                 // Current Arena Color Index
    public int RandomNumber { get; set; }                                                           // Random Number Index for Color Generation

    // Lists
    public List<GameObject> PlayerList { get; set; } = new();                                       // Player List
    public List<Transform> PlayerSpawnPoints { get; set; } = new();                                 // Player Spawn Points

    // Private Variables
    private string currentSceneId;                                                                  // Current Scene Id
    private string currentArenaId;                                                                  // Current Arena Id
    
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

    #region Enable/Disable Functions
    void OnDisable()
    {
        // Unsubscribe to Events
        EventManager.Instance.ResetMatch -= ReactivatePlayers;
        EventManager.Instance.ResetMatch -= GenerateArena;

        // Remove Additional Scenes
        SceneManager.UnloadSceneAsync(currentArenaId);
        SceneManager.UnloadSceneAsync("UIScene");
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        // Initialize Variables
        SetGameState(GameStates.SETTING_UP);
        GameCamera = Camera.GetComponent<Camera>();
        CameraController = Camera.GetComponent<CameraController>();
        
        // Add UI Scene
        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);

        // Subscribe to Events
        EventManager.Instance.ResetMatch += ReactivatePlayers;
        EventManager.Instance.ResetMatch += GenerateArena;

        // Randomized Number for Arena Color Generation
        RandomNumber = UnityEngine.Random.Range(0, ArenaColors.Length);

        // Initial Genration of Arena
        GenerateArena();
    }
    #endregion

    

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
            yield return SceneManager.UnloadSceneAsync(currentSceneId);

        Resources.UnloadUnusedAssets();
        yield return null;
        GC.Collect();
        yield return null;

        yield return SceneManager.LoadSceneAsync(arenaId, LoadSceneMode.Additive);

        currentSceneId = arenaId;
    }
    #endregion

    #region Public Functions
    // Round is Over
    // Declare Point
    public void RoundOver()
    {
        SetGameState(GameStates.ROUND_OVER);
        CameraController.CameraShake();
        EventManager.Instance.PlayerDied.Invoke();
    }

    // Sets Game State
    public void SetGameState(GameStates state)
    {
        CurrentGameState = state;
    }
    #endregion
}
