using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moves : UsingOnline
{
    public bool _isReady = true;

    [SerializeField]
    private float _coolDown;

    protected Animator _animator;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

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
