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

    // Update is called once per frame
    void Update()
    {
        //currentSpeed = Speed;
    }

    // Move Function
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        playerSetup.Rb.velocity = moveInput * currentSpeed;
    }
}
