using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    #region Enable/Disable Functions
    void OnEnable()
    {
        EventManager.Instance.MatchStart += StartCountdown;
    }

    void OnDisable()
    {
        EventManager.Instance.MatchStart -= StartCountdown;
    }
    #endregion

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.Instance.AllPlayersPresent)
        {
            StartCountdown();
        } 
    }
    #endregion

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        PanelManager.Instance.ActivatePanel("countdown-panel");

        float currentTime = 3f;

        while (currentTime > 0f)
        {
            UIController.Instance.CountdownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        UIController.Instance.CountdownText.text = "GO";
        yield return new WaitForSeconds(1f);

        GameManager.Instance.CurrentGameState = GameManager.GameStates.ROUND_START;
        PanelManager.Instance.ActivatePanel("score-board-panel");
    }
}
