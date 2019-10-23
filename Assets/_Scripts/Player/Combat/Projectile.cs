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

    protected int _damage = 0;
    protected float _range = 0;
    public PlayerController.Teams _teams;

    public virtual void Fired(int damage, float speed, float range, PlayerController.Teams teams)
    {
        _damage = damage;
        _speed = speed;
        _teams = teams;


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
            player.TakeDamage(_damage, _teams);
            if (!_destroyByPlayer || (player.currentTeam == _teams && _teams != PlayerController.Teams.noTeam))
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
