using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameShield : Shield
{
    public int _damage;

    void OnTriggerStay(Collider collider)
    {
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(_damage);
        }
    }
}
