using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController InputController = null;
    [SerializeField, Range(0f, 10f)] private float JumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int MaxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float DownwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float UpwardMovementMultiplier = 1.7f;

    private Rigidbody2D Body;
    private Ground Ground;
    private Vector2 Velocity;

    private int JumpPhase;
    private float DefaultGravityScale;

    private bool JumpInput;
    private bool OnGround;

    // Start is called before the first frame update
    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Ground = GetComponent<Ground>();

        DefaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        JumpInput |= InputController.RetrieveJumpInput();

            
        
    }

    private void FixedUpdate()
    {
        OnGround = Ground.GetOnGround();    
        Velocity = Body.velocity;

        if (OnGround)
        {
            JumpPhase = 0;
        }
        if (JumpInput)
        {
            
            JumpAction();
            JumpInput = false;
        }

        switch (Body.velocity.y) {
            case > 0:
                Body.gravityScale = UpwardMovementMultiplier;
                break;
            case < 0:
                Body.gravityScale = DownwardMovementMultiplier;
                break;
            case  0:
                Body.gravityScale = DefaultGravityScale;
                break;
        }

        Body.velocity = Velocity;


    }
    private void JumpAction()
    {
        if(OnGround || JumpPhase < MaxAirJumps)
        {
            JumpPhase += 1;
            float JumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * JumpHeight);
            if(Velocity.y > 0f)
            {
                JumpSpeed = Mathf.Max(JumpSpeed - Velocity.y, 0f);
            }
            Velocity.y += JumpSpeed;
        }
    }
}
