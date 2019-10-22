using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFlameShot : ProjectileBased
{
    private Projectile flameShot;
    private PlayerCombat _playerCombat;
    
    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void FlameShotLeftHand()
    {
        flameShot = Instantiate(_projectile, _spawnPosition[0].position, _spawnPosition[0].rotation);
        flameShot.transform.LookAt(_playerCombat.GetDirection());
        flameShot.Fired(_damage, _speed, _range, _playerCombat._playerController.currentTeam);
    }

    public void FlameShotRightHand()
    {
        flameShot = Instantiate(_projectile, _spawnPosition[1].position, _spawnPosition[1].rotation);
        flameShot.transform.LookAt(_playerCombat.GetDirection());
        flameShot.Fired(_damage, _speed, _range, _playerCombat._playerController.currentTeam);
    }
}
