using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected int _damage;
    protected float _speed;
    protected float _range;

    public void Fired(int damage, float speed, float range)
    {
        _damage = damage;
        _speed = speed;

        Invoke("Destroy", range / speed);
    }

    protected void Update()
    {
        if (_speed != 0)
        {
            Movement();
        }
    }

    protected void Movement()
    {
        transform.position += transform.forward * _speed;
    }

    protected void Destroy()
    {
        Destroy(gameObject);
    }
}
