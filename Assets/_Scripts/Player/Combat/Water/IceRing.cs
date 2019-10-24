using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRing : Ranged
{
    [SerializeField]
    protected float _iceDuration;

    [SerializeField]
    protected GameObject _iceEffect;

    private List<PlayerController> _players = new List<PlayerController>();

    private PlayerCombat _playerCombat;

    private GameObject _ice;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void DamageAllInRange()
    {
        Invoke("DestroyIce", _iceDuration);
        _ice = Instantiate(_iceEffect, _spawnPosition[0].position, _spawnPosition[0].rotation);
        _players.AddRange(FindObjectsOfType<PlayerController>());
        for (int i = 0; i < _players.Count; i++)
        {
            if (_playerCombat._playerController != _players[i] && Vector3.Distance(transform.position, _players[i].transform.position) < _range)
            {
                _players[i].TakeDamage(_damage, _playerCombat._playerController);
            }
        }
        _players.Clear();
    }

    private void DestroyIce()
    {
        Destroy(_ice);
    }
}
