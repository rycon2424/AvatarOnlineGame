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

    public IEnumerator LerpProjectile(int LerpPoint)
    {
        _spawnedProjectile = Instantiate(_projectile, new Vector3(_spawnPosition[0].position.x, _spawnPosition[0].position.y - _rayRange, _spawnPosition[0].position.z), _spawnPosition[0].rotation);
        if (!_playerCombat.GroundTest(_spawnPosition[LerpPoint], _spawnedProjectile, _rayRange))
        {
            Destroy(_spawnedProjectile.gameObject);
        }

        _lerp = true;

        while (_lerp && _spawnedProjectile != null)
        {
            _spawnedProjectile.transform.position = Vector3.Lerp(_spawnedProjectile.transform.position, _spawnPosition[LerpPoint].position, _lerpSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();

            if (!_playerCombat._playerController._isAlive)
            {
                _lerp = false;
                Destroy(_spawnedProjectile.gameObject);
            }
        }
    }
}
