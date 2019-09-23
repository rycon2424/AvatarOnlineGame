using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCombat : UsingOnline
{
    [SerializeField]
    private Moves _lightAttack;

    [SerializeField]
    private Moves _heavyAttack;

    [SerializeField]
    private Moves _ultimate;

    [SerializeField]
    private Moves _shield;

    public void LightAttack()
    {
        if (_lightAttack._isReady)
        {
            _lightAttack.UseMove();
        }
    }

    public void HeavyAttack()
    {
        if (_heavyAttack._isReady)
        {
            _heavyAttack.UseMove();
        }
    }

    public void Ultimate()
    {
        if (_ultimate._isReady)
        {
            _ultimate.UseMove();
        }
    }

    public void Shield()
    {
        if (_shield._isReady)
        {
            _shield.UseMove();
        }
    }
}
