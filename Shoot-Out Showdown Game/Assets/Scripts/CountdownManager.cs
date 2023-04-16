using System.Collections;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    // Private Variables
    private float countDownDelay;                                           // Time Delay Before the Actual Countdown

    private void OnEnable()
    {
        EventManager.Instance.MatchStart += StartCountdown;
    }

    private void OnDisable()
    {
        EventManager.Instance.MatchStart -= StartCountdown;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerManager.Instance.AllPlayersPresent)
            StartCountdown();
    }

    #region Countdown
    /// <summary>
    /// Initiates Countdown
    /// </summary>
    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
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
        GameManager.Instance.SetGameState(GameManager.GameStates.ROUND_START);
        PanelManager.Instance.ActivatePanel("score-board-panel");
    }
    #endregion
}
