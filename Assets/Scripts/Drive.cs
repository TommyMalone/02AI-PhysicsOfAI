using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : Tank
{
    [SerializeField] private float mouseSensitivity = 0.015f; // degrees per mouse pixel

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        Vector2 moveInput = InputSystem.actions["Move"].ReadValue<Vector2>();

        if (moveInput.y != 0)
        {
            MoveTank(moveInput.y > 0);
        }
        if (moveInput.x != 0)
        {
            RotateTank(moveInput.x > 0);
        }

        Vector2 lookInput = InputSystem.actions["Look"].ReadValue<Vector2>();

        if (lookInput.y != 0 || lookInput.x != 0)
        {
            RotateTurret(lookInput.y * mouseSensitivity, lookInput.x * mouseSensitivity);
        }
            
        if (InputSystem.actions["Attack"].WasReleasedThisFrame() || InputSystem.actions["Interact"].WasReleasedThisFrame() && CanFire())
        {
            FireShell();
        }
    }
    
}