using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedProjectile : Projectile
{
    [SerializeField]
    private float _groundedRange; 
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, _groundedRange))
        {
            return;
        }
        Destroy();
    }
}
