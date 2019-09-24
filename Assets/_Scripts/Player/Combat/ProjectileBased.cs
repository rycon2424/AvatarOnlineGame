using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBased : Ranged
{
    [SerializeField]
    protected Projectile _projectile;

    [SerializeField]
    protected Transform _spawnPosition;

    [SerializeField]
    protected float _speed;

}
