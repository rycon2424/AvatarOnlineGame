using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Moves : UsingOnline
{
    public bool _isReady = true;
    public bool _isReadyAnimating = true;

    public float _coolDown;

    [SerializeField]
    private AttackEnum _attack;

    public virtual void DoneAnimating(AttackEnum attack)
    {
        if (_attack == attack)
        {
            _isReadyAnimating = true;
            Invoke("DoneCooldown", _coolDown);
        }
    }

    private void DoneCooldown()
    {
        _isReady = true;
    }

    public virtual void UseMove(PlayerCombat playerCombat)
    {
        _isReadyAnimating = false;
        _isReady = false;
    }

}
