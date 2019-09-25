using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Moves : UsingOnline
{
    public bool _isReady = true;

    [SerializeField]
    private float _coolDown;

    public void DoneAnimating()
    {
        Invoke("DoneCooldown", _coolDown);
    }

    private void DoneCooldown()
    {
        _isReady = true;
    }

    public virtual void UseMove(Vector3 lookAt)
    {
        _isReady = false;
    }

}
