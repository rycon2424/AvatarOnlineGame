using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDash : Moves
{
    public GameObject dashEffect;
    public GameObject flameTrail;
    public BoxCollider damageTrigger;
    public int _damage;
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void EnableEffect()
    {
        dashEffect.SetActive(true);
        Debug.Log("enable trigger comp");
        damageTrigger.enabled = true;
    }

    public void DisableEffect()
    {
        Invoke("DisableDashEffect", 1.5f);
        damageTrigger.enabled = false;
    }

    void DisableDashEffect()
    {
        Debug.Log("disable trigger comp");
        dashEffect.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(_damage);
            damageTrigger.enabled = false;
        }
    }
}