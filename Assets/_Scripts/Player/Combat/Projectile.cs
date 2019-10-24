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

    [SerializeField]
    protected bool _destroyByBullet = true;

    [SerializeField]
    protected bool _destroyByPlayer = true;

    protected bool _hasBeenfired;

    protected int _damage = 0;
    protected float _range = 0;
    public PlayerController _owner;

    public virtual void Fired(int damage, float speed, float range, PlayerController owner)
    {
        if (_hasBeenfired)
        {
            return;
        }

        _damage = damage;
        _speed = speed;
        _owner = owner;
        _hasBeenfired = true;


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
            player.TakeDamage(_damage, _owner);
            if (!_destroyByPlayer || (player.currentTeam == _owner.currentTeam && _owner.currentTeam != PlayerController.Teams.noTeam))
            {
                return;
            }

        }
        if (_speed != 0)
        {
            Projectile projectile = collider.gameObject.GetComponent<Projectile>();
            Deflector deflector = collider.gameObject.GetComponent<Deflector>();
            if ((projectile != null && !_destroyByBullet) || deflector != null)
            {
                return;
            }
            Destroy();
        }
    }
}
