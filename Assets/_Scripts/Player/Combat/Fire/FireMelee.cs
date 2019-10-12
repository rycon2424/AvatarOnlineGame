using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMelee : RaycastBased
{
    public void MeleeAttack()
    {
        RaycastHit hit;
        Debug.DrawRay(_spawnPosition[0].position, _spawnPosition[0].forward, Color.red, 3);
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                return;
            }
            Debug.Log(hit.collider.name);
        }
    }
}
