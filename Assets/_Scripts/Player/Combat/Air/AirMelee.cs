using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMelee : RaycastBased
{
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void MeleeAttack()
    {
        RaycastHit hit;
        Debug.DrawRay(_spawnPosition[0].position, _spawnPosition[0].forward * _range, Color.red, 3);
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage, _playerCombat._playerController.currentTeam);
                return;
            }
            Debug.Log(hit.collider.name);
        }
    }
}
