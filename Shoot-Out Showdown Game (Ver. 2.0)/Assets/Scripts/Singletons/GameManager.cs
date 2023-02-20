using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        SETTING_UP,
        COUNTDOWN,
        ROUND_START,
        ROUND_OVER
    };

    public static GameManager Instance;

    public GameStates CurrentGameState { get; set; }                                                             // Current Game State

    #region Singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameStates.SETTING_UP;
    }
}
