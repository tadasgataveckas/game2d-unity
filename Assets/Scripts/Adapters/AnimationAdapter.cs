using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAdapter : MonoBehaviour
{
    private Weapon Weapon;
    private void Start()
    {
        Weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()
    {
        Weapon.AnimationFinishTrigger();
    }
}
