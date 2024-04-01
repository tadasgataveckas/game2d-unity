using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    private SO_AggressiveWeaponData aggressiveWeaponData;
    private List<IDamageable> detectedDamage = new List<IDamageable>(); 
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    protected override void Awake()
    {
        base.Awake();
        if(weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData =(SO_AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrongdata type on weapon");
        }
    }
    private void CheckMeleeAttack()
    {
        AttackDetailsStruct details = aggressiveWeaponData.AttackDetails[AttackCounter];
        foreach(IDamageable item in detectedDamage)
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetectedList(Collider2D collision)
    {
        
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if(damageable != null)
        {
            detectedDamage.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if(damageable != null)
        {
            detectedDamage.Remove(damageable);
        }
    }
}
