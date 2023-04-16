using UnityEngine;
using UnityEngine.InputSystem;

// Executes Shooting Mechanic
public class Shooting : MonoBehaviour
{
    public Gun CurrentGun;                                                      // Current Gun Being Used

    private PlayerSetup playerSetup;                                            // PlayerSetup Class Reference
    private Camera cam;                                                         // Camera Reference
    private Vector2 direction;                                                  // Direction
    private Vector2 aimInput;                                                   // Aim Input

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        cam = GameManager.Instance.GameCamera;
    }
    #endregion

    #region Update Functions
    void FixedUpdate()
    {
        if (playerSetup.CanMove())
        {
            if (playerSetup.PlayerInput.currentControlScheme == "Keyboard&Mouse")
                direction = aimInput - playerSetup.Rb.position;

            SetAngle();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    #endregion

    #region Player Input Events
    // Aim with Mouse
    public void MouseAim(InputAction.CallbackContext context)
    {
        if (playerSetup.PlayerInput.currentControlScheme == "Keyboard&Mouse")
        {
            aimInput = cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
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
            if (context.started)
                CurrentGun.Fire();
    }
    #endregion

    #region Public Functions
    // Set Player Rotation
    public void SetAngle()
    {
        float rotate = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, rotate);
    }
    #endregion
}
