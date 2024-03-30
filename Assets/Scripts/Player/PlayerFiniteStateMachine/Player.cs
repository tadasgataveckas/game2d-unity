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

    public PlayerTouchWallState WallTouchState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }

    public PlayerWallClimbState WallClimbState { get; private set; }

    public PlayerWallSlideState WallSlideState { get; private set; }

    public PlayerWallJumpState WallJumpState { get; private set; }

    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }

    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    #endregion

    #region Player components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D PlayerRigidbody { get; private set; }

    public BoxCollider2D PlayerBoxCollider { get; private set; }

    public PlayerInventory Inventory { get; private set; } 

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
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ceilingCheck;

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

        WallGrabState = new PlayerWallGrabState(this, StateMachine, PlayerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, PlayerData, "wallClimb");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, PlayerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, PlayerData, "inAir");

        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, PlayerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, PlayerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, PlayerData, "attack");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerBoxCollider = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<PlayerInventory>();
        PlayerDirection = 1;
        PrimaryAttackState.SetWeapon(Inventory.Weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.Weapons[(int)CombatInputs.secondary]);
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

    public void SetVelocityJump(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        VelocityData.Set(angle.x * velocity * direction, angle.y * velocity);
        PlayerRigidbody.velocity = VelocityData;
        CurrentVelocity = VelocityData;
    }

    public void SetBoxColliderOnCrouchEnter()
    {
        VelocityData.Set(PlayerBoxCollider.size.x, PlayerData.BoxColliderSizeEnter);
        PlayerBoxCollider.size = VelocityData;
    }

    public void SetBoxColliderOnCrouchExit()
    {
        VelocityData.Set(PlayerBoxCollider.size.x, PlayerData.BoxColliderSizeExit);
        PlayerBoxCollider.size = VelocityData;
    }
    #endregion

    #region Check methods

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, PlayerData.groundCheckRadius, PlayerData.groundMask);
    }

    public bool CheckCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, PlayerData.groundCheckRadius, PlayerData.groundMask);
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

    public bool CheckTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * PlayerDirection, PlayerData.wallCheckDistance, PlayerData.groundMask);
    }
    public bool CheckTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -PlayerDirection, PlayerData.wallCheckDistance, PlayerData.groundMask);
    }
    #endregion

}
