using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public List<string> PlayerNames { get; set; } = new();                                  // Player Name List

    public List<int> PointsToWin = new();                                                   // Winning Point List
    public int MaxScore { get; set; }                                                       // Maximum Score Value

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
