using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGroundedState : PlayerState
{
    protected int InputX;
    protected bool InputCrouch;
    protected bool IsTouchingCeiling;
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
        IsTouchingCeiling = Player.CheckCeiling();
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
        InputCrouch = Player.InputHandler.CrouchInput;
        if (Player.InputHandler.AttackInputs[(int)CombatInputs.primary] && !IsTouchingCeiling)
        {
            StateMachine.ChangeState(Player.PrimaryAttackState);
        }
        else if (Player.InputHandler.AttackInputs[(int)CombatInputs.secondary] &&!IsTouchingCeiling)
        {
            StateMachine.ChangeState(Player.SecondaryAttackState);
        }
        else if (JumpInput && Player.JumpState.CanJump())
        {
            //Player.InputHandler.SetJumpInputFalse();
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
