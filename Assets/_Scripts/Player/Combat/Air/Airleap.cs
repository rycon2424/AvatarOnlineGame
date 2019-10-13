using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airleap : RaycastBased
{
    [SerializeField]
    protected float _downwardRayRange;

    [SerializeField]
    protected float _betweenRayRange;

    [SerializeField]
    protected Projectile _cloud;

    [SerializeField]
    protected float _cloudDuration;

    [SerializeField]
    protected LayerMask layerMask;

    private GameObject _rayPoint;

    public void AirBlast()
    {
        _rayPoint = Instantiate(_spawnPosition[0].gameObject, _spawnPosition[0].position, _spawnPosition[0].rotation);
        RaycastHit main;
        if (Physics.Raycast(_rayPoint.transform.position, _rayPoint.transform.forward, out main, _range, layerMask))
        {
            StartCoroutine(CastRays(main.distance));
            return;
        }
        StartCoroutine(CastRays(_range));
    }

    private IEnumerator CastRays(float distance)
    {
        for (float i = 0; i < distance; i += _betweenRayRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(_rayPoint.transform.position + _rayPoint.transform.forward * i, -_rayPoint.transform.up, out hit, _downwardRayRange))
            {
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(_damage);
                }
                else
                {
                    Projectile rock = Instantiate(_cloud, hit.point, Random.rotation);
                    StartCoroutine(DestroyClouds(rock));
                }
            }
            yield return new WaitForEndOfFrame();
        }
        Destroy(_rayPoint.gameObject);
    }

    private IEnumerator DestroyClouds(Projectile projectile)
    {
        yield return new WaitForSeconds(_cloudDuration);
        projectile.Destroy();
    }
}
