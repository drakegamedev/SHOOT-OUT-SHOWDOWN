using System.Collections;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    // Private Variables
    private float countDownDelay;                                           // Time Delay Before the Actual Countdown

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
        if (PlayerManager.Instance.IsAllSet)
        {
            // Add a Small Delay to Process All the Values Before Initiating
            countDownDelay = 0.1f;
        }
        else
        {
            yield return new WaitForSeconds(0.75f);

            AudioManager.Instance.Play("all-set");
            PanelManager.Instance.ActivatePanel("all-set-panel");
            
            // Add 1.5 Seconds Delay to Indicate that Both Players are All Set
            countDownDelay = 1.5f;
        }

        yield return new WaitForSeconds(countDownDelay);

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
