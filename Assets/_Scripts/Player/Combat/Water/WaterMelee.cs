﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMelee : RaycastBased
{
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void Kick()
    {
        RaycastHit hit;
        Debug.DrawRay(_spawnPosition[0].position, _spawnPosition[0].forward, Color.red, 3);
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                return;
            }
        }
    }
}