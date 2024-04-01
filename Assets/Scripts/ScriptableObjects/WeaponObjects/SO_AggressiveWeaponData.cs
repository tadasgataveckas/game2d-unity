using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/WeaponAggro")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] private AttackDetailsStruct[] attackDetails;
    public AttackDetailsStruct[] AttackDetails { get => attackDetails; set => attackDetails = value; }
    private void OnEnable()
    {
        attackCount = attackDetails.Length;
        MovementSpeed = new float[attackCount];

        for (int i = 0; i < attackDetails.Length; i++)
        {
            MovementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}
