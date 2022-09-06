using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Executes Shooting Mechanic
public class Shooting : MonoBehaviour
{
    public Camera Cam;
    public Gun CurrentGun;

    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 aimInput;
    private PlayerInput playerInput;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Aim with Mouse
    public void MouseAim(InputAction.CallbackContext context)
    {
        if (playerInput.defaultControlScheme == "Keyboard&Mouse")
        {
            aimInput = Cam.ScreenToWorldPoint(context.ReadValue<Vector2>());

            direction = aimInput - rb.position;
            SetAngle();
        }
    }

    // Aim with Right Joystick
    public void ControllerAim(InputAction.CallbackContext context)
    {
        if (playerInput.defaultControlScheme == "Controller")
        {
            aimInput = context.ReadValue<Vector2>();

            // Maintain Angle
            // Prevents Snapping back to Zero Angle when Releasing Joystick
            if (aimInput.x != 0f && aimInput.y != 0f)
            {
                direction = aimInput;
                SetAngle();
            }
        }
    }

    // Shoot Out a Bullet
    public void Shoot(InputAction.CallbackContext context)
    {
        CurrentGun.Fire();
    }

    // Set Player Rotation
    public void SetAngle()
    {
        float rotate = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }
}
