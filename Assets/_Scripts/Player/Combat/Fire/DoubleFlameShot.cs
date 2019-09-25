using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleFlameShot : ProjectileBased
{
    [Header("Transforms + Prefab")]
    public Transform leftHand;
    public Transform rightHand;

    private Projectile flameShot;
    private PlayerCombat _playerCombat;
    
    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void FlameShotLeftHand()
    {
        flameShot = Instantiate(_projectile, leftHand.position, leftHand.rotation);
        flameShot.transform.LookAt(_playerCombat.GetDirection());
        flameShot.Fired(_damage, _speed, _range);
    }

    public void FlameShotRightHand()
    {
        flameShot = Instantiate(_projectile, rightHand.position, rightHand.rotation);
        flameShot.transform.LookAt(_playerCombat.GetDirection());
        flameShot.Fired(_damage, _speed, _range);
    }
}
