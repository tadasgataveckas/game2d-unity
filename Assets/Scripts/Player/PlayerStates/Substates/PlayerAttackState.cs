using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon Weapon;

    public PlayerAttackState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = true;
        Weapon.EnterWeapon();

    }

    public override void Exit()
    {
        base.Exit();
        Weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.Weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    #region Animation Triggers
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    #endregion
}
