using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWhip : RaycastBased
{
    private bool _castRays;

    public void StartWhip()
    {
        _castRays = true;
        StartCoroutine(CastRays());
    }

    public void EndWhip()
    {
        _castRays = false;
    }
    private IEnumerator CastRays()
    {
        while (_castRays)
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < _spawnPosition.Count; i++)
            {
                RaycastHit hit;
                //Debug.DrawRay(_spawnPosition[i].position, _spawnPosition[i].up, Color.blue, 5);
                if (Physics.Raycast(_spawnPosition[i].position, _spawnPosition[i].up, out hit, _range))
                {
                    PlayerController player = hit.collider.GetComponent<PlayerController>();
                    if (player != null)
                    {
                        player.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}
