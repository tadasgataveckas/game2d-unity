using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon Weapon;
    private float velocityToSet;
    private bool setVelocity;
    public PlayerAttackState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        setVelocity = false;

        Weapon.EnterWeapon();

    }

    public override void Exit()
    {
        base.Exit();
        //isAnimationFinished = true;
        Weapon.ExitWeapon();
        Player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (setVelocity)
        {
            
            SetPlayerVelocity(velocityToSet * Player.PlayerDirection);
                   
        }
        
    }

    public void SetWeapon(Weapon weapon)
    {
        this.Weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        Player.FlipCheck(Player.InputHandler.NormInputX);
        Player.SetVelocityX(velocity * Player.PlayerDirection);
        velocityToSet = velocity;
        setVelocity = false;
    }

    #region Animation Triggers
    public override void AnimationFinishTrigger()
    {
        
        base.AnimationFinishTrigger();
        
        isAbilityDone = true;
    }


    #endregion
}
