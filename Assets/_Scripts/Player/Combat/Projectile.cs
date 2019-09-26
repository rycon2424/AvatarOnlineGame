using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Mesh Collider if necessary")]
    public MeshRenderer _meshRenderer;

    [Header("Particles after destroy")]
    public GameObject destroyEffect;

    [HideInInspector]
    public float _speed = 0;

    protected int _damage = 0;
    protected float _range = 0;

    public void Fired(int damage, float speed, float range)
    {
        _damage = damage;
        _speed = speed;

        Invoke("Destroy", range / speed);
        StartCoroutine(Movement());
    }

    public virtual IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    public void Destroy()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider collider)
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
