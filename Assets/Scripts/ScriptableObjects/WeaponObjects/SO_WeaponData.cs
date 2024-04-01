using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName ="Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    public int attackCount { get; protected set; }
    public float[] MovementSpeed { get; protected set; }
}
