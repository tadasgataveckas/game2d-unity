using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private int InputX;
    private bool isGrounded;
    private bool jumpInput;
    private bool isJumping;
    private bool jumpInputStop;
    private bool isTouchingWall;
    private bool grabInput;
    public PlayerAirState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = Player.CheckGrounded();
        isTouchingWall = Player.CheckTouchingWall();

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        InputX = Player.InputHandler.NormInputX;
        jumpInput = Player.InputHandler.JumpInput;
        jumpInputStop = Player.InputHandler.JumpInputStop;
        grabInput = Player.InputHandler.GrabInput;

        CheckJumpInput();

        //stateupdates
        if (isGrounded && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.LandState);
        }
        else if (jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (isTouchingWall && grabInput)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
        else if (isTouchingWall && InputX == Player.PlayerDirection && Player.CurrentVelocity.y<=0)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            Player.FlipCheck(InputX);
            Player.SetVelocityX(PlayerData.MovementVelocity * InputX);
            Player.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.Animator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckJumpInput()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Player.CurrentVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    public void SetIsJumping() => isJumping = true;
}
