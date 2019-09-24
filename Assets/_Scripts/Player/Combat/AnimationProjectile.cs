using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProjectile : ProjectileBased
{
    [SerializeField]
    protected Transform _lerpPosition;

    [SerializeField]
    protected float _lerpSpeed;

    private Projectile _spawnedProjectile;
    private bool _lerp;
    private Vector3 _lookAt;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            LaunchProjectile();
            _isReady = true;
        }
    }

    public override void UseMove(Vector3 lookAt)
    {
        base.UseMove(lookAt);
        _lookAt = lookAt;
        _spawnedProjectile = Instantiate(_projectile, _spawnPosition.position, _spawnPosition.rotation);
        StartCoroutine(LerpProjectile());
    }

    public void LaunchProjectile()
    {
        _lerp = false;
        _spawnedProjectile.transform.LookAt(_lookAt);
        _spawnedProjectile.Fired(_damage, _speed, _range);
    }

    private IEnumerator LerpProjectile()
    {
        _lerp = true;

        while (_lerp)
        {
            yield return new WaitForEndOfFrame();
            _spawnedProjectile.transform.position = Vector3.Lerp(_spawnedProjectile.transform.position, _lerpPosition.position, _lerpSpeed * Time.deltaTime);
        }
    }
}
