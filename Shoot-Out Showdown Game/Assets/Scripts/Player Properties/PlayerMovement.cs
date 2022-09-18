using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Serves as Top Down Player Movement
public class PlayerMovement : MonoBehaviour
{
    public float Speed;

    private PlayerSetup playerSetup;
    private float currentSpeed;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        currentSpeed = Speed;
    }

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

    // Move Function
    public void Move(InputAction.CallbackContext context)
    {
        if (playerSetup.CanMove())
            moveInput = context.ReadValue<Vector2>();
    }
}
