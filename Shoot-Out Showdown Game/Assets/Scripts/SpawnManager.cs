using System.Collections.Generic;
using UnityEngine;

// Manages Player Spawning
public class SpawnManager : MonoBehaviour
{
    // Private Variables
    private List<Transform> spawnPoints = new();                                                 // Spawn Points Array

    // Start is called before the first frame update
    private void Start()
    {
        // Get All Children Spawn Points and Exclude the Parent
        for (int i = 0; i < GetComponentsInChildren<Transform>().Length; i++)
        {
            if (i != 0)
                spawnPoints.Add(GetComponentsInChildren<Transform>()[i]);
        }

        // Add SpawnPoints to Game Manager Spawn Point List
        for (int i = 0; i < spawnPoints.Count; i++)
            GameManager.Instance.PlayerSpawnPoints.Add(spawnPoints[i]);

        // Return players back to starting position
        // whenever a new arena has generated
        if (PlayerManager.Instance.AllPlayersPresent)
        {
            for (int i = 0; i < GameManager.Instance.PlayerList.Count; i++)
                GameManager.Instance.PlayerList[i].transform.position = GameManager.Instance.PlayerSpawnPoints[i].transform.position;
        }
    }
}
