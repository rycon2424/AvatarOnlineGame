using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMelee : RaycastBased
{
    [SerializeField]
    private int _wallDamage;

    [SerializeField]
    private int _wallSpeed;

    [SerializeField]
    private int _wallRange;

    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void Melee()
    {
        RaycastHit hit;
        Debug.DrawRay(_spawnPosition[0].position, _spawnPosition[0].forward, Color.red, 3);
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage, _playerCombat._playerController.currentTeam);
                return;
            }

            ShootingProjectile wall = hit.collider.gameObject.GetComponent<ShootingProjectile>();
            if (wall != null)
            {
                wall.Shoot(_playerCombat, _wallDamage, _wallSpeed, _wallRange);
            }
        }
    }
}
