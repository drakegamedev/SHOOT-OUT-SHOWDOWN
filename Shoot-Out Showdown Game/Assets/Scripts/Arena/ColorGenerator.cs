using UnityEngine;

// Generates a Random Color
public class ColorGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform walls;                              // Wall Holder Reference
    [SerializeField] private Transform obstacles;                          // Obstacle Holder Reference

    void Awake()
    {
        RandomNumberGenerator();
        Coloring();
    }

    /// <summary>
    /// Generates a Random Number
    /// </summary>
    private void RandomNumberGenerator()
    {
        // Make a Random Number Without Repetition
        while (GameManager.Instance.RandomNumber == GameManager.Instance.CurrentArenaColorIndex)
            GameManager.Instance.RandomNumber = Random.Range(0, GameManager.Instance.ArenaColors.Length);

        // Set Current Arena Color Index
        GameManager.Instance.CurrentArenaColorIndex = GameManager.Instance.RandomNumber;

        Debug.Log(GameManager.Instance.CurrentArenaColorIndex);
    }

    /// <summary>
    /// Provides a Color to the Walls and Obstacles
    /// </summary>
    private void Coloring()
    {
        // Gett All Walls and Obstacles from the Children
        SpriteRenderer[] wallSprites = walls.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] obstacleSprites = obstacles.GetComponentsInChildren<SpriteRenderer>();

        // Color Every Wall based on the Chosen Random Color
        foreach (SpriteRenderer sr in wallSprites)
            sr.color = GameManager.Instance.ArenaColors[GameManager.Instance.CurrentArenaColorIndex];

        // Color Every Obstacle based on the Chosen Random Color
        foreach (SpriteRenderer sr in obstacleSprites)
            sr.color = GameManager.Instance.ArenaColors[GameManager.Instance.CurrentArenaColorIndex];
    }
}
