using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : ProjectileBased
{
   public void WaterWave()
    {
        for (int i = 0; i < _spawnPosition.Count; i++)
        {
            Projectile projectile = Instantiate(_projectile, _spawnPosition[i].position, _spawnPosition[i].rotation);
            projectile.Fired(_damage,_speed,_range);
        }
    }
}
