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

    [SerializeField]
    private GameObject _gameObject;
    private void Update()
    {
        if (_particle != null)
        {
            _particle.startSize += _growspeed * Time.deltaTime;
        }
        if (_collider != null)
        {
            _collider.radius += (_growspeed / 11) * Time.deltaTime;
        }
        if (_gameObject != null)
        {
            _gameObject.transform.localScale += new Vector3(1,1,1) * _growspeed * Time.deltaTime;
        }
    }
}
