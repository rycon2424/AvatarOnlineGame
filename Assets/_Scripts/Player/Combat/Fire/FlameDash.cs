using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDash : Moves
{
    public GameObject dashEffect;
    public GameObject flameTrail;
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void EnableEffect()
    {
        dashEffect.SetActive(true);
    }

    public void DisableEffect()
    {
        Invoke("DisableDashEffect", 1.5f);
    }

    void DisableDashEffect()
    {
        dashEffect.SetActive(false);
    }

}