using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private int characterLimit;

    [Header("References")]
    [SerializeField] private TMP_InputField[] playerNames;
    [SerializeField] private TMP_Dropdown pointsToWinDropdown;
    
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
    public void OnPlayButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("game-setup-panel");
    }

    public void OnHowToPlayButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("tutorial-panel");
    }

    public void OnCreditsButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("credits-panel");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("You have quit the game.");
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        Application.Quit();
    }

    public void OnReturnButtonClicked()
    {
        AudioManager.Instance.PlayOneShot("on-button-pressed");
        PanelManager.Instance.ActivatePanel("main-menu-panel");
    }

    public void OnStartButtonClicked()
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            string name = playerNames[i].text;

            if (!string.IsNullOrEmpty(name))
            {
                AudioManager.Instance.PlayOneShot("on-button-pressed");
                PlayerData.Instance.PlayerNames.Add(name);
            }
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

    public void SetMaxScore()
    {
        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[pointsToWinDropdown.value];
        Debug.Log("Max Score: " + PlayerData.Instance.MaxScore);
    }
}
