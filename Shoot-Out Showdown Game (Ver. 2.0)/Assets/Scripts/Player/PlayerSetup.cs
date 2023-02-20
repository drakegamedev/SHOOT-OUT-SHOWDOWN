using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public Animator Animator { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // Cache in Class References and Components
        Rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInput = GetComponent<PlayerInput>();
        Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Checks if Player Can Move
    /// </summary>
    /// <returns></returns>
    public bool CanMove()
    {
        return GameManager.Instance.CurrentGameState == GameManager.GameStates.ROUND_START ||
               GameManager.Instance.CurrentGameState == GameManager.GameStates.ROUND_OVER;
    }
}
