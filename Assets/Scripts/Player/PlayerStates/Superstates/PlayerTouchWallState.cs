using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected int InputX;
    protected int InputY;
    protected bool grabInput;
    public PlayerTouchWallState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
        InputY = Player.InputHandler.NormInputY;
        grabInput = Player.InputHandler.GrabInput;
        if(isGrounded && !grabInput)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!isTouchingWall || (InputX != Player.PlayerDirection && !grabInput))
        {
            StateMachine.ChangeState(Player.AirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
