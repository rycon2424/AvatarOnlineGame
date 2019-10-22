using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingProjectile : Projectile
{
    [SerializeField]
    private Rigidbody _rigidbody;

    public override void Fired(int damage, float speed, float range , PlayerController.Teams teams)
    {
        base.Fired(damage, speed, range, teams);
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_speed != 0)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage, _teams);
            }
        }
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        if (_speed != 0)
        {
            Projectile projectile = collider.gameObject.GetComponent<Projectile>();
            if (projectile != null && !_destroyByBullet)
            {
                return;
            }
            Destroy();
        }
    }
}
