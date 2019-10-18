using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Shield
{
    [SerializeField]
    private float _damageReduction;

    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void UseShield()
    {
        _playerCombat._playerController._damageReduction = _damageReduction;
        Invoke("ShieldEnd", _shieldDuration);
    }

    public void ShieldEnd()
    {
        _playerCombat._playerController._damageReduction = 1;
        GetComponent<Animator>().Play("ShieldEnd");
    }
}