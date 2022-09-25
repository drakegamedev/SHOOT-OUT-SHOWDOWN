using UnityEngine;

// Generates a Random Color
public class ColorGenerator : MonoBehaviour
{
    public GameObject[] Obstacles;                              // Obstacles Array

    #region Initialization Functions
    void Awake()
    {
        // Find all Obstacle Objects Within the Arena
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        RandomNumberGenerator();
        ColorObstacles();
    }
    #endregion

    #region Private Functions
    // Generates a Random Number
    void RandomNumberGenerator()
    {
        // Make a Random Number Without Repetition
        while (GameManager.Instance.RandomNumber == GameManager.Instance.CurrentArenaColorIndex)
        {
            GameManager.Instance.RandomNumber = Random.Range(0, GameManager.Instance.ArenaColors.Length);
        }

        GameManager.Instance.CurrentArenaColorIndex = GameManager.Instance.RandomNumber;

        Debug.Log(GameManager.Instance.CurrentArenaColorIndex);
    }

    // Provides a Color to an Obstacle
    void ColorObstacles()
    {
        // Color Every Obstacle based on the Chosen Random Color
        foreach (GameObject go in Obstacles)
        {
            // Get Sprite Renderer Component
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();

            // Color the Object
            sr.color = GameManager.Instance.ArenaColors[GameManager.Instance.CurrentArenaColorIndex];
        }
    }
    #endregion
}
