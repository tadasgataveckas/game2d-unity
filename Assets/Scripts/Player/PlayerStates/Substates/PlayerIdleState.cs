using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (InputX != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (InputCrouch == true)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
