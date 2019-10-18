using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : Projectile
{
    [SerializeField]
    private Projectile _projectile;
    public void Shoot(PlayerCombat playerCombat, int damage, float speed, float range)
    {
        Projectile projectile = Instantiate(_projectile, transform.position, transform.rotation);
        projectile.transform.LookAt(playerCombat.GetDirection());
        projectile.Fired(damage, speed, range);
    }
}
