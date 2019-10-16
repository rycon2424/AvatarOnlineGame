using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirScooter : Moves
{
    [SerializeField]
    private float _airscooterDuration;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Animator _animator;

    private PlayerCombat _playerCombat;
    private bool _Move;
    public override void UseMove(PlayerCombat playerCombat)
    {
        _playerCombat = playerCombat;
        base.UseMove(playerCombat);
        Invoke("ScooterEnd",_airscooterDuration);
    }

    public IEnumerator Scooter()
    {

        _Move = true;

        while (_Move)
        {
            yield return new WaitForEndOfFrame();
            transform.position += transform.forward * _speed * Time.deltaTime;

            if (!_playerCombat._playerController._isAlive)
            {
                _Move = false;
            }
        }
    }

    private void ScooterEnd()
    {
        _Move = false;
        _animator.SetTrigger("EndScooter");
    }
}
