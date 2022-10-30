using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField[] PlayerNames;
    public TMP_Dropdown PointsToWinDropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("main-menu-panel");

        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[PointsToWinDropdown.value];
        PlayerData.Instance.PlayerNames.Clear();

        for (int i = 0; i < PlayerNames.Length; i++)
        {
            PlayerNames[i].characterLimit = 6;
        }
    }

    public void OnPlayButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("game-setup-panel");
    }

    public void OnReturnButtonClicked()
    {
        PanelManager.Instance.ActivatePanel("main-menu-panel");
    }

    public void OnStartButtonClicked()
    {
        for (int i = 0; i < PlayerNames.Length; i++)
        {
            string name = PlayerNames[i].text;

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

        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void DropdownInputData()
    {
        PlayerData.Instance.MaxScore = PlayerData.Instance.PointsToWin[PointsToWinDropdown.value];
        Debug.Log("Max Score: " + PlayerData.Instance.MaxScore);
    }
}
