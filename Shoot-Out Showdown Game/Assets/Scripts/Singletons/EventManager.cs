using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action PlayerDied;                       // Player Death Event
    public Action MatchReset;                       // Match Reset Event
    public Action MatchStart;                       // Match Start Event

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
}
