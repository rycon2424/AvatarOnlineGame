using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShield : Shield
{
    [SerializeField]
    private List<GameObject> _shieldParts;

    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void UseShield()
    {
        for (int i = 0; i < _shieldParts.Count; i++)
        {
            _shieldParts[i].SetActive(true);
        }
        //_playerCombat._playerController();
    }

    public void ShieldEnd()
    {
        for (int i = 0; i < _shieldParts.Count; i++)
        {
            _shieldParts[i].SetActive(false);
        }
    }
}
