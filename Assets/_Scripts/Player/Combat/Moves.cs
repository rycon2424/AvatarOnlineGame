using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moves : UsingOnline
{
    public bool _isReady = true;

    protected Animator _animator;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public abstract void UseMove();
}
