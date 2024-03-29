using UnityEngine;
using UnityEngine.InputSystem;

// Sets-Up Player Properties
public class PlayerSetup : MonoBehaviour
{
    public int PlayerNumber;                                                    // Player Number
    public Gun Gun;                                                             // Gun Reference

    public Rigidbody2D Rb { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public Animator Animator { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        // Add Player to the Player List
        GameManager.Instance.PlayerList.Add(gameObject);

        // Cache in Class References and Components
        Rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();

        // Set Position
        transform.position = GameManager.Instance.PlayerSpawnPoints[PlayerNumber - 1].position;
    }

    // Checks if Player Can Move
    public bool CanMove()
    {
        return GameManager.Instance.CurrentGameState == GameManager.GameStates.ROUND_START ||
               GameManager.Instance.CurrentGameState == GameManager.GameStates.ROUND_OVER;
    }
}
