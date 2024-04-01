using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator BaseAnimator;
    protected Animator WeaponAnimator;
    protected PlayerAttackState AttackState;
    protected int AttackCounter;
    protected virtual void Awake()
    {
        BaseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (AttackCounter >= weaponData.attackCount )
        {
            AttackCounter = 0;
        }

        BaseAnimator.SetBool("attack", true);
        WeaponAnimator.SetBool("attack", true);
        BaseAnimator.SetInteger("attackCounter", AttackCounter);
        WeaponAnimator.SetInteger("attackCounter", AttackCounter);

    }

    public virtual void ExitWeapon()
    {
        BaseAnimator.SetBool("attack", false);
        WeaponAnimator.SetBool("attack", false);
        AttackCounter++;
        gameObject.SetActive(false);
    }


    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        AttackState.AnimationFinishedTrigger();
        //Debug.Log("Animation finished weapon.cs");
    }

    public virtual void AnimationStartMovemenetTrigger()
    {
        AttackState.SetPlayerVelocity(weaponData.MovementSpeed[AttackCounter]);
        //Debug.Log("Animation start movement weapon.cs");
    }

    public virtual void AnimationStopMovementTrigger()
    {
        AttackState.SetPlayerVelocity(0f);
        //Debug.Log("Animation stop movement weapon.cs");
    }

    public virtual void AnimationActionTrigger()
    {

    }

    #endregion

    public void InitializeWeapon(PlayerAttackState AttackState)
    {
        this.AttackState = AttackState;
    }
}
