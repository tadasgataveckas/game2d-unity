using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] private InputController Controller = null;
    [SerializeField, Range(0f, 100f)] private float Max_Speed = 3f;
    [SerializeField, Range(0f, 100f)] private float Max_Acceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float Max_AirAcceleration = 40f;

    private Vector2 Direction;
    private Vector2 DesiredVelocity;
    private Vector2 CurrentVelocity;
    private Rigidbody2D Body;
    private Ground Ground;

    private float Max_Speed_Change;
    private float Acceleration;
    private bool onGround;
    
    // Start is called before the first frame update
    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        Ground = GetComponent<Ground>();
    }

    // Update is called once per frame
    void Update()
    {
        Direction.x = Controller.RetrieveMoveInput();
        DesiredVelocity = new Vector2 (Direction.x, 0f) * Mathf.Max(Max_Speed - Ground.GetFriction(),0f);
    }

    private void FixedUpdate()
    {
        onGround = Ground.GetOnGround();
        CurrentVelocity = Body.velocity;

        Acceleration = onGround ? Max_Acceleration : Max_AirAcceleration;
        Max_Speed_Change = Acceleration * Time.deltaTime;
        CurrentVelocity.x = Mathf.MoveTowards(CurrentVelocity.x, DesiredVelocity.x, Max_Speed_Change);
        Body.velocity = CurrentVelocity;
    }
}
