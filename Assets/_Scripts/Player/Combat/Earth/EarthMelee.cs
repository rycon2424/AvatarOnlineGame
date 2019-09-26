using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMelee : RaycastBased
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
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                return;
            }

            WallProjectile wall = hit.collider.gameObject.GetComponent<WallProjectile>();
            if (wall != null)
            {
                Vector3 direction = _playerCombat.GetDirection();
                wall.transform.LookAt(new Vector3(direction.x, wall.transform.position.y, direction.z));
                wall.Fired(_wallDamage, _wallSpeed, _wallRange);
                return;
            }
        }
    }
}

