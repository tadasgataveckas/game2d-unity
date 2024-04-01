using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAdapter : MonoBehaviour
{
    private AggressiveWeapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("ontriggerenter2d");
        weapon.AddToDetectedList(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("ontriggerexit2d");
        weapon.RemoveFromDetected(collision);
    }
}
