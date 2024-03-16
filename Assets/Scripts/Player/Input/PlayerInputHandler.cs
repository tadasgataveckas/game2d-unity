using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }

    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    public bool GrabInput {  get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.1f;

    private float jumpInputStartTime;

    private void Update()
    {
        CheckInputHoldTime();
    }
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
            JumpInput = true;
            jumpInputStartTime = Time.time;
            JumpInputStop = false;

        }
        if (context.performed)
        {
           // Debug.Log("Jump called(held down)");
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void ResetJumpInput() => JumpInput = false;

    private void CheckInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }
}
