using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlash : ProjectileBased
{
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void FireAirSlash()
    {
        Projectile projectile = Instantiate(_projectile, _spawnPosition[0].position, _spawnPosition[0].rotation);
        projectile.transform.LookAt(_playerCombat.GetDirection());
        projectile.Fired(_damage, _speed, _range, _playerCombat._playerController);
    }
}
