using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameManager.Instance.PlayerSpawnPoints.Add(SpawnPoints[i]);
        }

        if (GameManager.Instance.allPlayers)
        {
            for (int i = 0; i < GameManager.Instance.PlayerList.Count; i++)
            {
                GameManager.Instance.PlayerList[i].transform.position = GameManager.Instance.PlayerSpawnPoints[i].transform.position;
            }
        }
    }
}
