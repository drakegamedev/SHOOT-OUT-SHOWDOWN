using UnityEngine;
using UnityEngine.InputSystem;

// Serves as Top Down Player Movement
public class PlayerMovement : MonoBehaviour
{
    public float Speed;                                                     // Player Default Speed

    // Private Variables
    private PlayerSetup playerSetup;                                        // PlayerSetup Class Reference                                        
    private float currentSpeed;                                             // Current Player Speed
    private Vector2 moveInput;                                              // Move Inputs

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        currentSpeed = Speed;
    }
    #endregion

    #region Update Functions
    void FixedUpdate()
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
    #endregion

    #region Player Input Events
    // Move Function
    public void Move(InputAction.CallbackContext context)
    {
        if (playerSetup.CanMove())
            moveInput = context.ReadValue<Vector2>();
    }
    #endregion
}
