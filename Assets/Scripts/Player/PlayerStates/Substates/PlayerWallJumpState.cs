using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.SetJumpInputFalse();
        Player.JumpState.ResetJumpCount();
        Player.SetVelocityJump(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, wallJumpDirection);
        Player.FlipCheck(wallJumpDirection);
        Player.JumpState.DecreaseJumpCount();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Player.Animator.SetFloat("yVelocity", Player.CurrentVelocity.y);
        Player.Animator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
        if(Time.time >= StartTime + PlayerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void FindWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Player.PlayerDirection;
        }
        else
        {
            wallJumpDirection = Player.PlayerDirection;
        }

    }
}
