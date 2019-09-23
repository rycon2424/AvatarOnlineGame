using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBased : Ranged
{
    [SerializeField]
    protected Projectile _projectile;

    [SerializeField]
    protected float _speed;

    public override void UseMove()
    {

    }
}
