using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGroundedState : PlayerState
{
    protected int InputX;
    private bool JumpInput;
    private bool IsGrounded;
    private bool IsTouchingWall;
    private bool GrabInput;
    public PlayerGroundedState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = Player.CheckGrounded();
        IsTouchingWall = Player.CheckTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetJumpCount();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        InputX = Player.InputHandler.NormInputX;
        JumpInput = Player.InputHandler.JumpInput;
        GrabInput = Player.InputHandler.GrabInput;

        if (JumpInput && Player.JumpState.CanJump())
        {
            Player.InputHandler.ResetJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!IsGrounded)
        {
            StateMachine.ChangeState(Player.AirState);
            //no jump after walking off ledge without jumping first
            //Player.JumpState.DecreaseJumpCount();
        }
        else if (IsTouchingWall && GrabInput)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
