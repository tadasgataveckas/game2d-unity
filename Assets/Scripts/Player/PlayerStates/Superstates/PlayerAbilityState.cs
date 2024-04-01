using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    private bool isGrounded;

    protected bool isAnimationDone;
    public PlayerAbilityState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname) : base(player, statemachine, playerdata, animationboolname)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = Player.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
        isAnimationDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Debug.Log("Ability done?" + isAbilityDone.ToString());
        //Debug.Log("Animation done?" + isAnimationFinished.ToString());
        //(Player.CurrentVelocity.y < 0.01f))
        if (isAbilityDone || isAnimationFinished)
        {
            if (isGrounded && (Player.CurrentVelocity.y < 0.01f))
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (isGrounded && (Player.CurrentVelocity.y) > 0f)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (!isGrounded )
            {
                StateMachine.ChangeState(Player.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void AnimationFinishTrigger()
    {
       
        isAnimationDone = true;
    }
}
