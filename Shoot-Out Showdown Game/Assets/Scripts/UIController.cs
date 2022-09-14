using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerItem
    {
        public string Id { get; set; }
        public Image[] HPCircle;
        public int Score;
        public Color[] HPColors;
    }

    public TextMeshProUGUI[] PlayerNameTexts;
    public TextMeshProUGUI[] PlayerScoreTexts;

    private GameObject playerVictor;

    public int StartingScore;

    public PlayerItem[] PlayerItems;

    public GameObject ASD;

    void OnEnable()
    {
        EventManager.Instance.PlayerDied += SetScore;
    }

    void OnDisable()
    {
        EventManager.Instance.PlayerDied -= SetScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Player Item Properties
        for (int i = 0; i < PlayerItems.Length; i++)
        {
            // Initialize Names
            PlayerItems[i].Id = PlayerData.Instance.PlayerNames[i];
            PlayerNameTexts[i].text = PlayerItems[i].Id;

            // Initialize Score
            PlayerItems[i].Score = StartingScore;
            PlayerScoreTexts[i].text = PlayerItems[i].Score.ToString("0");

            // Initialize HP Containers
            for (int j = 0; j < PlayerItems[i].HPCircle.Length; j++)
            {
                PlayerItems[i].HPCircle[j].color = PlayerItems[i].HPColors[0];
            }
        }

        // UI Controller Reference
        GameManager.Instance.UiController = this;
    }

    // Update Player Health Container
    public void UpdateHealth(int playerIndex, float playerHealth)
    {
        // Health Container Reference
        PlayerItem playerItem = PlayerItems[playerIndex - 1];

        // Update Every HP Image
        for (int i = 0; i < playerItem.HPCircle.Length; i++)
        {
            if (i < playerHealth)
            {
                // Set Transparency of that Image to 100%
                playerItem.HPCircle[i].color = playerItem.HPColors[0];
            }
            else
            {
                // Set Transparency of that Image to 0%
                playerItem.HPCircle[i].color = playerItem.HPColors[1];
            }
        }
    }

    public void SetScore()
    {
        StartCoroutine(SetPlayerScore());
    }

    IEnumerator SetPlayerScore()
    {
        yield return new WaitForSeconds(3f);

        // Find the Player Victor
        playerVictor = GameObject.FindGameObjectWithTag("Player");

        PlayerSetup playerSetup = playerVictor.GetComponent<PlayerSetup>();
        int index = playerSetup.PlayerNumber - 1;

        // Add Score
        PlayerItems[index].Score++;
        PlayerScoreTexts[index].text = PlayerItems[index].Score.ToString("0");
        GameObject gameObject1 = ObjectPooler.Instance.SpawnFromPool("player-1-score-effect", PlayerScoreTexts[index].transform.position, Quaternion.identity);
        gameObject1.transform.localScale = Vector3.one;
        //Instantiate();
        playerVictor = null;

        yield return new WaitForSeconds(2f);

        // Reset All Properties and Generate new Arena
        GameManager.Instance.CurrentGameState = GameManager.GameStates.ROUND_START;
        GameManager.Instance.PlayerSpawnPoints.Clear();

        GameManager.Instance.ReactivatePlayers();
        GameManager.Instance.GenerateArena();
    }
}
