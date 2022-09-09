using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public string[] PlayerIds;

    public GameStates CurrentGameState;

    public List<GameObject> PlayerList = new();

    public UIController UiController;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGame()
    {
        CurrentGameState = GameStates.ROUND_OVER;
        CameraController.CameraShake();
        EventManager.Instance.PlayerDied.Invoke();
        StartCoroutine(SetPlayerScore());
    }

    public void SpawnPlayers(Transform position, Transform rotation)
    {
        for (int i = 0; i < PlayerIds.Length; i++)
        {
            //ObjectPooler
        }
    }

    IEnumerator SetPlayerScore()
    {
        yield return new WaitForSeconds(2f);

        // Set Score

        int index = PlayerList[0].GetComponent<PlayerSetup>().PlayerNumber;

        PlayerData.Instance.PlayerScores[index - 1]++;
        UiController.PlayerScoreTexts[index - 1].text = PlayerData.Instance.PlayerScores[index - 1].ToString("0");


        yield return new WaitForSeconds(3f);

        SceneLoader.Instance.LoadScene("GameScene");
    }
}
