using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Moves : UsingOnline
{
    public bool _isReady = true;
    public bool _isReadyAnimating = true;

    [SerializeField]
    private float _coolDown;

    [SerializeField]
    private AttackEnum _attack;

    public void DoneAnimating(AttackEnum attack)
    {
        Debug.Log(attack + " attack");
        Debug.Log(_attack + " _attack");
        if (_attack == attack)
        {
            Debug.Log("succeeded with " + attack);
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
