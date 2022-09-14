using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] SpawnPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        // Add SpawnPoints to Game Manager Spawn Point List
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            GameManager.Instance.PlayerSpawnPoints.Add(SpawnPoints[i]);
        }

        // Return players back to starting position
        // whenever a new arena has generated
        if (GameManager.Instance.allPlayersPresent)
        {
            for (int i = 0; i < GameManager.Instance.PlayerList.Count; i++)
            {
                GameManager.Instance.PlayerList[i].transform.position = GameManager.Instance.PlayerSpawnPoints[i].transform.position;
            }
        }
    }
}
