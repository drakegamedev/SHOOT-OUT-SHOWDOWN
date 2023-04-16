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
    [SerializeField] private GameObject mainCamera;                                                 // Camera GameObject Reference
    public GameObject[] PlayerPrefabs;                                                              // Player Prefabs Array
    
    [Header("Properties")]
    public Color[] ArenaColors;                                                                     // Arena Color
    [SerializeField] private string[] arenaIds;                                                     // Arena ID's

    public GameStates CurrentGameState { get; private set; }                                        // Current Game State
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

    private void OnDisable()
    {
        // Unsubscribe to Events
        EventManager.Instance.MatchReset -= ReactivatePlayers;
        EventManager.Instance.MatchReset -= GenerateArena;

        // Remove Additional Scenes
        SceneManager.UnloadSceneAsync(currentArenaId);
        SceneManager.UnloadSceneAsync("UIScene");
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize Variables
        SetGameState(GameStates.SETTING_UP);
        GameCamera = mainCamera.GetComponent<Camera>();
        CameraController = mainCamera.GetComponent<CameraController>();

        // Add UI Scene
        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);

        // Subscribe to Events
        EventManager.Instance.MatchReset += ReactivatePlayers;
        EventManager.Instance.MatchReset += GenerateArena;

        // Randomized Number for Arena Color Generation
        RandomNumber = UnityEngine.Random.Range(0, ArenaColors.Length);

        AudioManager.Instance.Play("game-bgm");

        // Initial Generation of Arena
        GenerateArena();
    }

    /// <summary>
    /// Reset Player Properties
    /// </summary>
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
    /// <summary>
    /// Randomly Generates an Arena
    /// </summary>
    public void GenerateArena()
    {
        // Randomize Arena Index
        int randomArena = UnityEngine.Random.Range(0, arenaIds.Length);

        // Ensure that Next Generated Arena will not be the Same as the Previous One
        while (arenaIds[randomArena] == currentArenaId)
            randomArena = UnityEngine.Random.Range(0, arenaIds.Length);

        currentArenaId = arenaIds[randomArena];

        Debug.Log(currentArenaId);

        StartCoroutine(InitializeArena(arenaIds[randomArena]));
    }

    /// <summary>
    /// Initializes All Arena Properties
    /// </summary>
    /// <param name="arenaId"></param>
    /// <returns></returns>
    private IEnumerator InitializeArena(string arenaId)
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

    /// <summary>
    /// Round is Over
    /// Declare Point
    /// </summary>
    public void RoundOver()
    {
        SetGameState(GameStates.ROUND_OVER);
        CameraController.CameraShake();
        EventManager.Instance.PlayerDied.Invoke();
    }

    /// <summary>
    /// Sets Game State
    /// </summary>
    /// <param name="state"></param>
    public void SetGameState(GameStates state)
    {
        CurrentGameState = state;
    }
}
