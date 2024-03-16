using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityY(-PlayerData.wallSlideVelocity);

        if(grabInput && InputY == 0)
        {
            StateMachine.ChangeState(Player.WallClimbState);
        }
    }
}
