using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationProjectile : ProjectileBased
{
    [SerializeField]
    protected Transform _lerpPosition;

    [SerializeField]
    protected float _lerpSpeed;

    private Projectile _spawnedProjectile;
    private bool _lerp;
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        _spawnedProjectile = Instantiate(_projectile, _spawnPosition.position, _spawnPosition.rotation);
    }

    public void LaunchProjectile()
    {
        _lerp = false;
        _spawnedProjectile.transform.LookAt(_playerCombat.GetDirection());
        _spawnedProjectile.Fired(_damage, _speed, _range);
    }

    public IEnumerator LerpProjectile()
    {
        _lerp = true;

        while (_lerp)
        {
            yield return new WaitForEndOfFrame();
            _spawnedProjectile.transform.position = Vector3.Lerp(_spawnedProjectile.transform.position, _lerpPosition.position, _lerpSpeed * Time.deltaTime);
        }
    }
}
