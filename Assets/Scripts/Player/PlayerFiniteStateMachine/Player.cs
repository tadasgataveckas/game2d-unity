using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAirState AirState { get; private set; }


    #endregion

    #region Player components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D PlayerRigidbody { get; private set; }

    [SerializeField]
    private PlayerData PlayerData;

    #endregion

    #region Player variables
    public Vector2 CurrentVelocity { get; private set; }

    public int PlayerDirection { get; private set; }

    private Vector2 VelocityData;

    #endregion

    #region Check variables
    [SerializeField]
    private Transform groundCheck;

    #endregion
    #region Unity callback methods
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, PlayerData, "inAir");
        AirState = new PlayerAirState(this, StateMachine, PlayerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, PlayerData, "land");

        
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerDirection = 1;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = PlayerRigidbody.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set methods
    public void SetVelocityX(float velocity)
    {
        VelocityData.Set(velocity, CurrentVelocity.y);
        PlayerRigidbody.velocity = VelocityData;
        CurrentVelocity = VelocityData;

    }

    public void SetVelocityY(float velocity)
    {
        VelocityData.Set(CurrentVelocity.x, velocity);
        PlayerRigidbody.velocity = VelocityData;
        CurrentVelocity = VelocityData;
    }
    #endregion

    #region Check methods

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, PlayerData.groundCheckRadius, PlayerData.groundMask);
    }
    private void Flip()
    {
        PlayerDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    public void FlipCheck(int InputX)
    {
        if( InputX != 0 & InputX != PlayerDirection )
        {
            Flip();
        }
    }

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();
    #endregion

}
