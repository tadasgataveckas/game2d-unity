using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchWallState
{
    private Vector2 freezePosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
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
    }

    public override void Enter()
    {
        base.Enter();
       
        freezePosition = Player.transform.position;

        FreezePlayer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Player.SetVelocityX(0);
        //Player.SetVelocityY(0);
        
        if (!isExitingState)
        {
            FreezePlayer();
            if (InputY > 0)
            {
                StateMachine.ChangeState(Player.WallClimbState);

            }
            else if (InputY < 0 || !grabInput)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    private void FreezePlayer()
    {
        Player.transform.position = freezePosition;
        Player.SetVelocityX(0);
        Player.SetVelocityY(0);
    }
}
