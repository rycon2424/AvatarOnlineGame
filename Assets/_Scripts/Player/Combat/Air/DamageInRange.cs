using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInRange : Ranged
{
    private List<PlayerController> _players = new List<PlayerController>();
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void DamageAllInRange()
    {
        _players.AddRange(FindObjectsOfType<PlayerController>());
        for (int i = 0; i < _players.Count; i++)
        {
            if (_playerCombat._playerController != _players[i] && Vector3.Distance(transform.position, _players[i].transform.position) < _range)
            {
                _players[i].TakeDamage(_damage, _playerCombat._playerController.currentTeam);
            }
        }
        _players.Clear();
    }
}
