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
            StartCountdown();
    }
    #endregion

    #region Countdown
    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Add a Small Delay to Process All the Values Before Initiating
        yield return new WaitForSeconds(0.1f);

        // Open Countdown Panel
        PanelManager.Instance.ActivatePanel("countdown-panel");

        // Set Timer
        float currentTime = 3f;

        // Indicate Time Left in the Countdown Text
        while (currentTime > 0f)
        {
            UIController.Instance.CountdownText.text = currentTime.ToString("0");
            AudioManager.Instance.Play("countdown");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        // Go!
        UIController.Instance.CountdownText.text = "GO";
        AudioManager.Instance.Play("game-start");

        yield return new WaitForSeconds(1f);

        // Start the Round
        GameManager.Instance.CurrentGameState = GameManager.GameStates.ROUND_START;
        PanelManager.Instance.ActivatePanel("score-board-panel");
    }
    #endregion
}
