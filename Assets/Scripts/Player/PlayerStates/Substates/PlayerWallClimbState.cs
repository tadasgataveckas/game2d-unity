using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Player.SetVelocityY(PlayerData.wallClimbVelocity);

        if (InputY != 1)
        {
            StateMachine.ChangeState(Player.WallGrabState);
        }
    }
}
