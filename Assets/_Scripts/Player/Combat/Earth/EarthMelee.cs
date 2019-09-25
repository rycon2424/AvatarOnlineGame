using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMelee : RaycastBased
{
    public void Melee()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }
        DoneAnimating();
    }
}

