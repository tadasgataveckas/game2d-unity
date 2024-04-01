using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;
    private Animator animator;

    public void Damage(float amount)
    {
        Debug.Log(amount + "Damage taken by: " + this.name);
        //Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        animator.SetTrigger("damage");
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
