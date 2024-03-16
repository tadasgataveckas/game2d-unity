using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    
    public PlayerLandState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(InputX != 0 && !isExitingState)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
        else if (isAnimationFinished && !isExitingState)
        {
            StateMachine.ChangeState(Player.IdleState);
            //Animation -> land.anim -> create animation event gale - > pasirinkti funckija AnimationFinishedTrigger();
        }
    }
}
