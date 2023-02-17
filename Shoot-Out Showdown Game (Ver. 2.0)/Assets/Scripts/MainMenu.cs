using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int nameCharacterLimit;
    [SerializeField] private TMP_InputField[] playerNames;
    [SerializeField] private TMP_Dropdown pointsToWinDropdown;

    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("main-menu-panel");

        // Initialize Player Names and Max Score Value
        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[pointsToWinDropdown.value];
        PlayerData.Instance.PlayerNames.Clear();

        // Set Character Limit for PlayerInputs
        for (int i = 0; i < playerNames.Length; i++)
        {
            playerNames[i].characterLimit = nameCharacterLimit;
        }
    }

    #region UI Buttons
    public void OnPlayButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("game-setup-panel");
    }

    public void OnHowToPlayButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("tutorial-panel");
    }

    public void OnCreditsButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("credits-panel");
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("You have quit the game.");
        Application.Quit();
    }

    public void OnReturnButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("main-menu-panel");
    }

    public void OnStartButtonClicked()
    {
        for (int i = 0; i < playerNames.Length; i++)
        {
            string name = playerNames[i].text;

            if (!string.IsNullOrEmpty(name))
            {
                PlayerData.Instance.PlayerNames.Add(name);
            }
            else
            {
                Debug.Log("Invalid Player Inputs");
                PlayerData.Instance.PlayerNames.Clear();
                return;
            }
        }

        string[] scenes = { "GameScene", "GameUIScene" };

        SceneLoader.Instance.LoadScene(scenes);
    }
    #endregion

    public void SetMaxScore()
    {
        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[pointsToWinDropdown.value];
        Debug.Log("Max Score: " + PlayerData.Instance.MaxScore);
    }
}
