using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
        jumpsLeft = PlayerData.jumpCount;
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.SetJumpInputFalse();
        Player.SetVelocityY(PlayerData.jumpVelocity);
        isAbilityDone = true;
        jumpsLeft--;
        Player.AirState.SetIsJumping();
    }

    public bool CanJump() => jumpsLeft>0 ? true : false;

    public void ResetJumpCount() => jumpsLeft = PlayerData.jumpCount;

    public void DecreaseJumpCount() => jumpsLeft--;
}
