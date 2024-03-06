using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }

    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    //context started = calltime, performed = continious calltime, cancelled = stopped.
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
           // Debug.Log("Jump called");
        }
        if (context.performed)
        {
           // Debug.Log("Jump called(held down)");
        }
        if (context.canceled)
        {
           // Debug.Log("Jump button released");
        }
    }
}
