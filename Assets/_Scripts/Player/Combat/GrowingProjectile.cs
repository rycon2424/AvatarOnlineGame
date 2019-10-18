using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingProjectile : Projectile
{
    [SerializeField]
    private float _growspeed;

    [SerializeField]
    private SphereCollider _collider;

    [SerializeField]
    private ParticleSystem _particle;
    private void Update()
    {
        _collider.radius += (_growspeed/11) * Time.deltaTime;
        _particle.startSize += _growspeed * Time.deltaTime;
    }
}
