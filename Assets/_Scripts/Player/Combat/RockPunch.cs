using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RockPunch : ProjectileBased
{
    [SerializeField]
    protected float _rayRange;

    [SerializeField]
    protected float _lerpSpeed;

    private Projectile _spawnedProjectile;
    private bool _lerp;
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        _spawnedProjectile = Instantiate(_projectile, new Vector3(_spawnPosition.position.x, _spawnPosition.position.y - _rayRange, _spawnPosition.position.z), _spawnPosition.rotation);
        if (!_playerCombat.GroundTest(_spawnPosition, _spawnedProjectile, _rayRange))
        {
            Destroy(_spawnedProjectile.gameObject);
        }
    }

    public void LaunchProjectile()
    {
        if (_spawnedProjectile != null)
        {
            _lerp = false;
            _spawnedProjectile.transform.LookAt(_playerCombat.GetDirection());
            _spawnedProjectile.Fired(_damage, _speed, _range);
        }
    }

    public IEnumerator LerpProjectile()
    {
        _lerp = true;

        while (_lerp && _spawnedProjectile != null)
        {
            yield return new WaitForEndOfFrame();
            _spawnedProjectile.transform.position = Vector3.Lerp(_spawnedProjectile.transform.position, _spawnPosition.position, _lerpSpeed * Time.deltaTime);
        }
    }
}
