using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }



    #endregion

    #region Player components
    public Animator Animator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RigidbodyPlayer { get; private set; }

    [SerializeField]
    private PlayerData PlayerData;

    #endregion

    #region Player variables
    public Vector2 CurrentVelocity { get; private set; }

    public int PlayerDirection { get; private set; }

    private Vector2 VelocityData;

    #endregion

    #region Unity callback methods
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "move");
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RigidbodyPlayer = GetComponent<Rigidbody2D>();
        PlayerDirection = 1;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RigidbodyPlayer.velocity;
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
        RigidbodyPlayer.velocity = VelocityData;
        CurrentVelocity = VelocityData;

    }
    #endregion

    #region Check methods
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
    #endregion

}
