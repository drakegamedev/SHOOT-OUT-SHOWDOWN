using UnityEngine;
using UnityEngine.InputSystem;

// Serves as Top-Down Player Movement
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;                            // Player Default Speed

    // Private Variables
    private PlayerSetup playerSetup;                                        // PlayerSetup Class Reference                                        
    private float currentSpeed;                                             // Current Player Speed
    private Vector2 moveInput;                                              // Move Inputs

    // Start is called before the first frame update
    private void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        currentSpeed = defaultSpeed;
    }

    private void FixedUpdate()
    {
        if (playerSetup.CanMove())
        {
            playerSetup.Rb.velocity = moveInput * currentSpeed;
        }
        else
        {
            playerSetup.Rb.velocity = new Vector2(0f, 0f);
            moveInput = new Vector2(0f, 0f);
        }
    }

    #region Player Input Events
    // Move Function
    public void Move(InputAction.CallbackContext context)
    {
        if (playerSetup.CanMove())
            moveInput = context.ReadValue<Vector2>();
    }
    #endregion
}
