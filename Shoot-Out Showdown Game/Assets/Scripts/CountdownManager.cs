using System.Collections;
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

    #region Countdown
    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Open Countdown Panel
        PanelManager.Instance.ActivatePanel("countdown-panel");

        float currentTime = 3f;

        // Indicate Time Left in the Countdown Text
        while (currentTime > 0f)
        {
            UIController.Instance.CountdownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        UIController.Instance.CountdownText.text = "GO";
        yield return new WaitForSeconds(1f);

        // Start the Round
        GameManager.Instance.CurrentGameState = GameManager.GameStates.ROUND_START;
        PanelManager.Instance.ActivatePanel("score-board-panel");
    }
    #endregion
}
