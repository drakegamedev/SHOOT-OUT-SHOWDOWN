using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int characterLimit;                                          // Character Limit Amount

    [Header("References")]
    [SerializeField] private TMP_InputField[] playerNames;                                // Player Name Input Field References
    [SerializeField] private TMP_Dropdown pointsToWinDropdown;                            // Dropdown Menu Reference
    
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("main-menu-panel");

        AudioManager.Instance.Play("main-menu-bgm");

        // Initialize Player Names and Max Score Value
        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[pointsToWinDropdown.value];
        PlayerData.Instance.PlayerNames.Clear();

        // Set Character Limit for PlayerInputs
        for (int i = 0; i < playerNames.Length; i++)
            playerNames[i].characterLimit = characterLimit;
    }

    #region UI Buttons
    /// <summary>
    /// Play Button
    /// </summary>
    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("game-setup-panel");
    }

    /// <summary>
    /// Tutorial Button
    /// </summary>
    public void OnHowToPlayButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("tutorial-panel");
    }

    /// <summary>
    /// Credits Button
    /// </summary>
    public void OnCreditsButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("credits-panel");
    }

    /// <summary>
    /// Quit Button
    /// </summary>
    public void OnQuitButtonClicked()
    {
        Debug.Log("You have quit the game.");
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        Application.Quit();
    }

    /// <summary>
    /// Return Button
    /// </summary>
    public void OnReturnButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("main-menu-panel");
    }

    /// <summary>
    /// Start Game Button
    /// </summary>
    public void OnStartButtonClicked()
    {
        // Check if all Player Name Inputs Have Been Filled Out
        for (int i = 0; i < playerNames.Length; i++)
        {
            string name = playerNames[i].text;

            // Add this Name to the List
            if (!string.IsNullOrEmpty(name))
            {
                AudioManager.Instance.PlayOneShot("on-button-pressed");
                PlayerData.Instance.PlayerNames.Add(name);
            }
            // Declare Void
            else
            {
                Debug.Log("Invalid Player Inputs");
                AudioManager.Instance.PlayOneShot("invalid");
                PlayerData.Instance.PlayerNames.Clear();
                return;
            }
        }

        string[] scenes = { "GameScene" };

        SceneLoader.Instance.LoadScene(scenes);
    }
    #endregion

    /// <summary>
    /// Sets the Maximum Score
    /// </summary>
    public void SetMaxScore()
    {
        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[pointsToWinDropdown.value];
        Debug.Log("Max Score: " + PlayerData.Instance.MaxScore);
    }
}
