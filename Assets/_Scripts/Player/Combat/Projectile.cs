﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MeshRenderer _meshRenderer;

    protected int _damage = 0;
    protected float _speed = 0;
    protected float _range = 0;

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
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    protected void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(_damage);
        }
        if (_speed != 0)
        {
            Destroy();
        }
    }
}
