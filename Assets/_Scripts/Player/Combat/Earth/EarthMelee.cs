using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMelee : RaycastBased
{
    public void Melee()
    {
        RaycastHit hit;
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            Debug.DrawRay(_spawnPosition[0].position, _spawnPosition[0].forward, Color.red, 1);

            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }
    }
}

