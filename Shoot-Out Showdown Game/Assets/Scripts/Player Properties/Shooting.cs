using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Executes Shooting Mechanic
public class Shooting : MonoBehaviour
{
    // FIX SHOOTING BUG LATER
    // WHEN GENERATING A NEW ARENA, SOME PLAYERS CAN'T SHOOT ANYMORE
    
    public Gun CurrentGun;

    private Vector2 direction;
    private Vector2 aimInput;
    private PlayerSetup playerSetup;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        cam = GameManager.Instance.GameCamera;
    }

    // Update is called once per frame
    void Update()
    {
        SetAngle();
    }

    // Aim with Mouse
    public void MouseAim(InputAction.CallbackContext context)
    {
        if (playerSetup.PlayerInput.currentControlScheme == "Keyboard&Mouse")
        {
            aimInput = cam.ScreenToWorldPoint(context.ReadValue<Vector2>());

            direction = aimInput - playerSetup.Rb.position;
        }
    }

    // Aim with Right Joystick
    public void ControllerAim(InputAction.CallbackContext context)
    {
        if (playerSetup.PlayerInput.currentControlScheme == "Controller")
        {
            aimInput = context.ReadValue<Vector2>();

            // Maintain Angle
            // Prevents Snapping back to Zero Angle when Releasing Joystick
            if (aimInput.x != 0f && aimInput.y != 0f)
            {
                direction = aimInput;
            }
        }
    }

    // Shoot Out a Bullet
    public void Shoot(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameStates.ROUND_START)
        {
            CurrentGun.Fire();
        }
    }

    // Set Player Rotation
    public void SetAngle()
    {
        float rotate = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(0, 0, rotate);
    }
}
