using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    
    public PlayerCrouchMoveState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Player.SetBoxColliderOnCrouchEnter();
    }

    public override void Exit()
    {
        base.Exit();
        Player.SetBoxColliderOnCrouchExit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Player.FlipCheck(InputX);
            Player.SetVelocityX((PlayerData.MovementVelocity * PlayerData.CrouchSpeedMultiplier) * InputX);
        
            if (!InputCrouch && InputX != 0 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (InputX == 0)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
        }
    }
}
