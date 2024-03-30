using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator BaseAnimator;
    protected Animator WeaponAnimator;
    protected PlayerAttackState AttackState;
    protected virtual void Start()
    {
        BaseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);
        BaseAnimator.SetBool("attack", true);
        WeaponAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        BaseAnimator.SetBool("attack", false);
        WeaponAnimator.SetBool("attack", false);
        gameObject.SetActive(false);
    }


    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        AttackState.AnimationFinishedTrigger();
        
    }
    #endregion

    public void InitializeWeapon(PlayerAttackState AttackState)
    {
        this.AttackState = AttackState;
    }
}
