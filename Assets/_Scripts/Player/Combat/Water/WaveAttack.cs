using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : ProjectileBased
{
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void WaterWave()
    {
        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            Projectile projectile = Instantiate(_projectile, _spawnPosition[i].position, _spawnPosition[i].rotation);
            projectile.Fired(_damage,_speed,_range, _playerCombat._playerController);
        }
    }
}
