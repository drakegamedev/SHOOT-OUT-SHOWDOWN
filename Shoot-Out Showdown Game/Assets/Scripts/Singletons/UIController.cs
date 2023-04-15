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
        public int Score;
        public Color[] HPColors;
    }

    public static UIController Instance;

    public TextMeshProUGUI[] PlayerNameTexts;                                               // Player Names
    public GameObject[] PlayerScoreTexts;                                                   // Player Score Object Reference 
    public GameObject[] PlayerReadyItems;                                                   // Player Ready Item Object Reference

    public PlayerItem[] PlayerItems;                                                        // Player Items
    public TextMeshProUGUI CountdownText;                                                   // Countdown Text
    public TextMeshProUGUI ObjectiveText;                                                   // Objective Text
    public TextMeshProUGUI PlayerWinnerText;                                                // Player Winner Text
    public Image PlayerWinnerImage;                                                         // Player Winner Image

    // Private Variables
    private int startingScore;                                                              // Starting Score for Both Players
    private GameObject playerVictor;                                                        // Player Victor for the Round

    #region Enable/Disable Functions
    void OnEnable()
    {
        EventManager.Instance.PlayerDied += SetScore;
    }

    void OnDisable()
    {
        EventManager.Instance.PlayerDied -= SetScore;
    }
    #endregion

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
        PanelManager.Instance.ActivatePanel("setup-panel");

        // Setup Starting Score
        startingScore = 0;

        // Initialize Player Item Properties
        for (int i = 0; i < PlayerItems.Length; i++)
        {
            // Initialize Names
            PlayerItems[i].Id = PlayerData.Instance.PlayerNames[i];
            PlayerNameTexts[i].text = PlayerItems[i].Id;

            // Initialize Score
            PlayerItems[i].Score = startingScore;
            PlayerScoreTexts[i].GetComponent<TextMeshProUGUI>().text = PlayerItems[i].Score.ToString("0");

            // Initialize HP Containers
            for (int j = 0; j < PlayerItems[i].HPCircle.Length; j++)
            {
                PlayerItems[i].HPCircle[j].color = PlayerItems[i].HPColors[0];
            }
        }

        // Set Objective Text
        ObjectiveText.text = "First to " + PlayerData.Instance.MaxScore + " Kills Wins!";
    }
    #endregion

    #region UI Button Functions
    public void OnRetryButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");

        string[] scenes = { "GameScene" };

        SceneLoader.Instance.LoadScene(scenes);
    }
    
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
        PlayerItems[index].Score++;
        PlayerScoreTexts[index].GetComponent<TextMeshProUGUI>().text = PlayerItems[index].Score.ToString("0");
        PlayerScoreTexts[index].GetComponent<Animator>().SetTrigger("SetScore");
        AudioManager.Instance.Play("set-score");

        // Nullify Player Victor
        playerVictor = null;

        yield return new WaitForSeconds(2f);

        // Game, Set, Match!
        if (PlayerItems[index].Score >= PlayerData.Instance.MaxScore)
        {
            Debug.Log(PlayerNameTexts[index].text + " wins!");

            // Announce the Overall Winner!
            PlayerWinnerText.text = PlayerNameTexts[index].text + " wins!";
            PlayerWinnerText.color = PlayerNameTexts[index].color;
            PlayerWinnerImage.color = PlayerNameTexts[index].color;

            // Destroy Every Player at End Game  
            for (int i = 0; i < GameManager.Instance.PlayerList.Count; i++)
            {
                Destroy(GameManager.Instance.PlayerList[i]);
            }

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
            EventManager.Instance.ResetMatch.Invoke();
        }
    }
    #endregion

    #region Public Functions
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

    // Visual Indication that a Player has Successfully
    // Joined the Game and is Ready
    public void PlayerIsReady(int count)
    {
        Animator readyItemAnimator = PlayerReadyItems[count].GetComponent<Animator>();
        PlayerReadyItem playerReadyItem = PlayerReadyItems[count].GetComponent<PlayerReadyItem>();

        // Indicate Player Ready
        readyItemAnimator.SetBool("isReady", true);
        playerReadyItem.Ready();
    }
    #endregion
}
