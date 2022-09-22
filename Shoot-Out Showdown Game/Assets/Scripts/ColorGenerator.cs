using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generates a Random Color
public class ColorGenerator : MonoBehaviour
{
    public GameObject[] Obstacles;                              // Obstacles Array

    // Private Variables
    private int currentArenaColorIndex;

    #region Initialization Functions
    void Awake()
    {
        // Find all Obstacle Objects Within the Arena
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        // Generate a Random Number
        int randomNumber = Random.Range(0, GameManager.Instance.ArenaColors.Length);

        while (randomNumber == currentArenaColorIndex)
        {
            randomNumber = Random.Range(0, GameManager.Instance.ArenaColors.Length);
        }

        currentArenaColorIndex = randomNumber;

        Debug.Log(currentArenaColorIndex);

        // Color Every Obstacle based on the Chosen Random Color
        foreach (GameObject go in Obstacles)
        {
            // Get Sprite Renderer Component
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();

            // Color the Object
            sr.color = GameManager.Instance.ArenaColors[currentArenaColorIndex];
        }
    }
    #endregion
}
