using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;

    protected float StartTime;

    private string AnimationBoolName;

    public PlayerState(Player player, PlayerStateMachine statemachine, PlayerData playerdata, string animationboolname)
    {
        Player = player;
        StateMachine = statemachine;
        PlayerData = playerdata;
        AnimationBoolName = animationboolname;
    }
    #region Methods
    //virtual = gali buti perrasyta paveldimu klasiu
    public virtual void Enter()
    {
        DoChecks();
        Player.Animator.SetBool(AnimationBoolName, true);
        StartTime = Time.time;
            
        UnityEngine.Debug.Log(AnimationBoolName);
    }

    public virtual void Exit()
    {
        Player.Animator.SetBool(AnimationBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    #endregion
}
