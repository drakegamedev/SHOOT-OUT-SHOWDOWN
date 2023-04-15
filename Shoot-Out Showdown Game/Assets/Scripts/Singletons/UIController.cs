using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Controls In-Game UI
public class UIController : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerItem
    {
        public string Id { get; set; }
        public Image[] HPCircle;
        public int Score { get; set; }
        public Color[] HPColors;
    }

    public static UIController Instance;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI[] playerNameTexts;                             // Player Names
    [SerializeField] private GameObject[] playerScoreTexts;                                 // Player Score Object Reference 
    [SerializeField] private GameObject[] playerReadyItems;                                 // Player Ready Item Object Reference

    [SerializeField] private PlayerItem[] playerItems;                                      // Player Items
    public TextMeshProUGUI CountdownText;                                                   // Countdown Text
    [SerializeField] private TextMeshProUGUI objectiveText;                                 // Objective Text
    [SerializeField] private TextMeshProUGUI playerWinnerText;                              // Player Winner Text
    [SerializeField] private Image playerWinnerImage;                                       // Player Winner Image

    // Private Variables
    private int startingScore;                                                              // Starting Score for Both Players
    private GameObject playerVictor;                                                        // Player Victor for the Round

    private void OnEnable()
    {
        EventManager.Instance.PlayerDied += SetScore;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerDied -= SetScore;
    }

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
        PanelManager.Instance.ActivatePanel("setup-panel");

        // Setup Starting Score
        startingScore = 0;

        // Initialize Player Item Properties
        for (int i = 0; i < playerItems.Length; i++)
        {
            // Initialize Names
            playerItems[i].Id = PlayerData.Instance.PlayerNames[i];
            playerNameTexts[i].text = playerItems[i].Id;

            // Initialize Score
            playerItems[i].Score = startingScore;
            playerScoreTexts[i].GetComponent<TextMeshProUGUI>().text = playerItems[i].Score.ToString("0");

            // Initialize HP Containers
            for (int j = 0; j < playerItems[i].HPCircle.Length; j++)
                playerItems[i].HPCircle[j].color = playerItems[i].HPColors[0];
        }

        // Set Objective Text
        objectiveText.text = "First to " + PlayerData.Instance.MaxScore + " Kills Wins!";
    }

    #region UI Button Functions
    /// <summary>
    /// Restarts the Game
    /// </summary>
    public void OnRetryButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");

        string[] scenes = { "GameScene" };

        SceneLoader.Instance.LoadScene(scenes);
    }
    
    /// <summary>
    /// Redirects to MainMenuScene
    /// </summary>
    public void OnMainMenuButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");

        string[] scenes = { "MainMenuScene" };

        SceneLoader.Instance.LoadScene(scenes);
    }
    #endregion

    #region Score Setting
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
        playerItems[index].Score++;
        playerScoreTexts[index].GetComponent<TextMeshProUGUI>().text = playerItems[index].Score.ToString("0");
        playerScoreTexts[index].GetComponent<Animator>().SetTrigger("SetScore");
        AudioManager.Instance.Play("set-score");

        // Nullify Player Victor
        playerVictor = null;

        yield return new WaitForSeconds(2f);

        // Game, Set, Match!
        if (playerItems[index].Score >= PlayerData.Instance.MaxScore)
        {
            Debug.Log(playerNameTexts[index].text + " wins!");

            // Announce the Overall Winner!
            playerWinnerText.text = playerNameTexts[index].text + " wins!";
            playerWinnerText.color = playerNameTexts[index].color;
            playerWinnerImage.color = playerNameTexts[index].color;

            // Destroy Every Player at End Game  
            for (int i = 0; i < GameManager.Instance.PlayerList.Count; i++)
                Destroy(GameManager.Instance.PlayerList[i]);

            // Stop BGM
            AudioManager.Instance.Stop("game-bgm");

            // Show Results
            PanelManager.Instance.ActivatePanel("results-panel");

            // Play Victory Jingle
            AudioManager.Instance.Play("victory");
        }
        else
        {
            // Reset All Properties and Generate new Arena
            PanelManager.Instance.ActivatePanel("black-panel");
            GameManager.Instance.PlayerSpawnPoints.Clear();
            EventManager.Instance.MatchReset.Invoke();
        }
    }
    #endregion

    /// <summary>
    /// Update Player Health Container
    /// </summary>
    /// <param name="playerIndex"></param>
    /// <param name="playerHealth"></param>
    public void UpdateHealth(int playerIndex, float playerHealth)
    {
        // Health Container Reference
        PlayerItem playerItem = playerItems[playerIndex - 1];

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

    /// <summary>
    /// Visual Indication that a Player has Successfully
    /// Joined the Game and is Ready
    /// </summary>
    /// <param name="count"></param>
    public void PlayerIsReady(int count)
    {
        Animator readyItemAnimator = playerReadyItems[count].GetComponent<Animator>();
        PlayerReadyItem playerReadyItem = playerReadyItems[count].GetComponent<PlayerReadyItem>();

        // Indicate Player Ready
        readyItemAnimator.SetBool("isReady", true);
        playerReadyItem.Ready();
    }
}
