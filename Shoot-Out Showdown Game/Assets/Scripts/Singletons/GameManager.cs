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
        ROUND_START,
        ROUND_OVER
    };
    
    public static GameManager Instance;

    public GameObject Camera;
    public Camera GameCamera { get; private set; }
    public CameraController CameraController { get; private set; }

    public GameObject[] PlayerPrefabs;


    public GameStates CurrentGameState;

    public List<GameObject> PlayerList = new();

    public UIController UiController;

    public PlayerInputManager PlayerInputManager;

    private string currentSceneId;

    // Spawning Points
    public List<Transform> PlayerSpawnPoints = new();

    public bool allPlayers { get; private set; }

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
        CurrentGameState = GameStates.ROUND_START;
        GameCamera = Camera.GetComponent<Camera>();
        CameraController = Camera.GetComponent<CameraController>();
        PlayerInputManager.playerPrefab = PlayerPrefabs[0];
        
        allPlayers = false;

        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);

        GenerateArena();
    }

    public void Join()
    {
        PlayerInputManager.playerPrefab = PlayerPrefabs[1];

        if (PlayerInputManager.playerCount == PlayerInputManager.maxPlayerCount)
        {
            PlayerInputManager.DisableJoining();
            allPlayers = true;
        }
    }

    public void SetGame()
    {
        CurrentGameState = GameStates.ROUND_OVER;
        CameraController.CameraShake();
        EventManager.Instance.PlayerDied.Invoke();
    }

    

    public void ReactivatePlayers()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].SetActive(true);

            PlayerHealth playerHealth = PlayerList[i].GetComponent<PlayerHealth>();
            playerHealth.ResetHealth();
        }
    }

    #region Level Generation
    public void GenerateArena()
    {
        StartCoroutine(InitializeArena("Arena1"));
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
