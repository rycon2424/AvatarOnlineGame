using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProjectile : Projectile
{
    public override IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position += new Vector3(transform.forward.x * _speed * Time.deltaTime, 0, transform.forward.z * _speed * Time.deltaTime);
        }
    }
}
