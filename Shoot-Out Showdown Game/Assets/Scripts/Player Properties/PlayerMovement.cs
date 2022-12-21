using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Serves as Top Down Player Movement
public class PlayerMovement : MonoBehaviour
{
    public float Speed;                                                     // Player Default Speed
    public float DashSpeed;                                                 // Dash Speed Value

    // Private Variables
    private PlayerSetup playerSetup;                                        // PlayerSetup Class Reference                                        
    private float currentSpeed;                                             // Current Player Speed
    private bool isDashing;
    private Vector2 moveInput;                                              // Move Inputs

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        currentSpeed = Speed;
        isDashing = false;
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

    // Dash Function
    public void Dash(InputAction.CallbackContext context)
    {
        if (!playerSetup.CanMove())
            return;
        
        if (context.performed && !isDashing)
            StartCoroutine(Dashing());
    }
    #endregion

    IEnumerator Dashing()
    {
        // Execute Dash
        isDashing = true;
        currentSpeed = DashSpeed;
        AudioManager.Instance.Play("dash");

        yield return new WaitForSeconds(0.05f);

        currentSpeed = Speed;

        // Dash Cooldown
        yield return new WaitForSeconds(1.5f);

        isDashing = false;
    }
}
