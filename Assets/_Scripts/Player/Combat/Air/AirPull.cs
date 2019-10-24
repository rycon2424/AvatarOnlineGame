using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPull : Ranged
{

    [SerializeField]
    protected float _lerpSpeed;

    private List<PlayerController> _players = new List<PlayerController>();
    private PlayerCombat _playerCombat;
    private bool _lerp;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public IEnumerator LerpPlayers()
    {
        _lerp = true;
        _players.AddRange(FindObjectsOfType<PlayerController>());

        while (_lerp)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_playerCombat._playerController != _players[i] && Vector3.Distance(_spawnPosition[0].position, _players[i].transform.position) < _range)
                {
                    _players[i].transform.position = Vector3.Lerp(_players[i].transform.position, _spawnPosition[0].position, _lerpSpeed * Time.deltaTime);
                }
            }
            yield return new WaitForEndOfFrame();

            if (!_playerCombat._playerController._isAlive)
            {
                EndPull();
            }
        }
    }

    public void EndPull()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_playerCombat._playerController != _players[i] && Vector3.Distance(_spawnPosition[0].position, _players[i].transform.position) < _range)
            {
                _players[i].TakeDamage(_damage, _playerCombat._playerController);
            }
        }

        _players.Clear();
        _lerp = false;
    }
}
