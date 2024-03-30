using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    
    public PlayerMoveState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Player.FlipCheck(InputX);
        Player.SetVelocityX(PlayerData.MovementVelocity * InputX);
        if (!isExitingState)
        {
            if (InputX == 0)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (InputCrouch == true)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
