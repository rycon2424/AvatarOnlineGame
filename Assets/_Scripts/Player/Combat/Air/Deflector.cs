using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    public DeflectiveShield _deflectiveShield;

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            _deflectiveShield.DeflectProjectile(projectile);
        }
        Projectile projectile2 = other.gameObject.GetComponentInParent<Projectile>();
        if (projectile2 != null)
        {
            _deflectiveShield.DeflectProjectile(projectile2);
        }
    }
}
