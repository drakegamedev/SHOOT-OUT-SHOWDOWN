using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI[] PlayerNameTexts;
    public TextMeshProUGUI[] PlayerScoreTexts;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerData.Instance.PlayerNames.Count; i++)
        {
            PlayerNameTexts[i].text = PlayerData.Instance.PlayerNames[i];
        }

        for (int i = 0; i < PlayerData.Instance.PlayerScores.Length; i++)
        {
            PlayerScoreTexts[i].text = PlayerData.Instance.PlayerScores[i].ToString("0");
        }

        

        GameManager.Instance.UiController = this;
    }

    public void SetScore()
    {

    }

    IEnumerator SetPlayerScore()
    {
        yield return new WaitForSeconds(2f);
        
        PlayerData.Instance.PlayerScores[0]++;
        PlayerScoreTexts[0].text = PlayerData.Instance.PlayerScores[0].ToString("0");
    }
}
