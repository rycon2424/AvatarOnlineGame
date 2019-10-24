using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWhip : RaycastBased
{
    private bool _castRays;
    private List<PlayerController> _hitPlayer = new List<PlayerController>();
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void StartWhip()
    {
        _castRays = true;
        StartCoroutine(CastRays());
    }

    public void EndWhip()
    {
        _castRays = false;
        _hitPlayer.Clear();
    }
    private IEnumerator CastRays()
    {
        while (_castRays)
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < _spawnPosition.Count; i++)
            {
                RaycastHit hit;
                Debug.DrawRay(_spawnPosition[i].position, _spawnPosition[i].up, Color.blue, 5);
                if (Physics.Raycast(_spawnPosition[i].position, _spawnPosition[i].up, out hit, _range))
                {
                    CheckPlayer(hit);
                }
            }
        }
    }

    private void CheckPlayer(RaycastHit hit)
    {
        PlayerController player = hit.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            for (int i = 0; i < _hitPlayer.Count; i++)
            {
                if (player == _hitPlayer[i])
                {
                    return;
                }
            }
            player.TakeDamage(_damage, _playerCombat._playerController);
            _hitPlayer.Add(player);
        }
    }
}
