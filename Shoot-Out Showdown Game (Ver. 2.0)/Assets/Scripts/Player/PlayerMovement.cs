using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;                                   // Player Default Speed

    // Private Variables
    private PlayerSetup playerSetup;                                        // PlayerSetup Class Reference                                        
    private float currentSpeed;                                             // Current Player Speed
    private Vector2 moveInput;                                              // Move Inputs

    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        currentSpeed = Speed;
    }

    void FixedUpdate()
    {
        playerSetup.Rb.velocity = moveInput * currentSpeed;

        if (playerSetup.CanMove())
        {
            //playerSetup.Rb.velocity = moveInput * currentSpeed;
        }
        else
        {
            //.Rb.velocity = new Vector2(0f, 0f);
            //moveInput = new Vector2(0f, 0f);
        }
    }

    #region Player Input Events
    // Move Function
    public void Move(InputAction.CallbackContext context)
    {
        //if (!playerSetup.CanMove())
        //return;
        

       moveInput = context.ReadValue<Vector2>();

        Debug.Log(moveInput);
    }
    #endregion
}
