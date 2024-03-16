using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName ="Data/Player Data/Base Data")]


public class PlayerData : ScriptableObject
{
    [Header("Move state")]
    public float MovementVelocity = 10f;

    [Header("Jump state")]
    public float jumpVelocity = 17f;
    public int jumpCount = 1;

    [Header("In air state")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask groundMask;
    public float wallCheckDistance = 1f;

    [Header("Wall Slide state")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb state")]
    public float wallClimbVelocity = 3f;
}
