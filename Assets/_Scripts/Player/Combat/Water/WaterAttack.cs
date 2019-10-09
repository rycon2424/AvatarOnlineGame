using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAttack : ProjectileBased
{
    private PlayerCombat _playerCombat;
    private Animator _animator;
    private bool _LeftHand;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        ChooseHand();
    }

    private void ChooseHand()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        if (_LeftHand)
        {
            _animator.Play("LeftAttack");
            _LeftHand = false;
        }
        _animator.Play("RightAttack");
        _LeftHand = true;
    }

    public void LeftHand()
    {
        Projectile projectile = Instantiate(_projectile, _spawnPosition[0].position, _spawnPosition[0].rotation);
        projectile.transform.LookAt(_playerCombat.GetDirection());
        projectile.Fired(_damage, _speed, _range);
    }

    public void RightHand()
    {
        Projectile projectile = Instantiate(_projectile, _spawnPosition[1].position, _spawnPosition[1].rotation);
        projectile.transform.LookAt(_playerCombat.GetDirection());
        projectile.Fired(_damage, _speed, _range);
    }
}