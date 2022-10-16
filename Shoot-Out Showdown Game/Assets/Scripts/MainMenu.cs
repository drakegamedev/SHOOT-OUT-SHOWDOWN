using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField[] PlayerNames;
    
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.ActivatePanel("main-menu-panel");

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
}
