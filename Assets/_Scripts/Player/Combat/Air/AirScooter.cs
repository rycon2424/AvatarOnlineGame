using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirScooter : Moves
{
    [SerializeField]
    private float _airscooterDuration;

    [SerializeField]
    private Animator _animator;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        Invoke("ScooterEnd",_airscooterDuration);
    }

    private void ScooterEnd()
    {
        _animator.SetTrigger("EndScooter");
    }
}
