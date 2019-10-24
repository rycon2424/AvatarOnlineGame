using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplotion : ProjectileBased
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _lineTime;

    private PlayerCombat _playerCombat;
    private Projectile _firedProjectile;
    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void ShootExplotion()
    {
        _spawnPosition[0].LookAt(_playerCombat.GetDirection());
        RaycastHit hit;
        _lineRenderer.SetPosition(0, _spawnPosition[0].position);
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            _lineRenderer.SetPosition(1, hit.point);
            _firedProjectile = Instantiate(_projectile, hit.point, transform.rotation);
        }
        else
        {
            _lineRenderer.SetPosition(1, _spawnPosition[0].position + _spawnPosition[0].forward * _range);
            _firedProjectile = Instantiate(_projectile, _spawnPosition[0].position + _spawnPosition[0].forward * _range, transform.rotation);
        }
        _firedProjectile.Fired(_damage,_speed, _speed, _playerCombat._playerController);
        Invoke("RemoveLine", _lineTime);
    }

    private void RemoveLine()
    {
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
    }
}
