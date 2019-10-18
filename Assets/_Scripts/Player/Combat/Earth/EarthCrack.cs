using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCrack : RaycastBased
{
    [SerializeField]
    protected float _downwardRayRange;

    [SerializeField]
    protected float _betweenRayRange;

    [SerializeField]
    protected Projectile _rock;

    [SerializeField]
    protected float _rockDuration;

    [SerializeField]
    protected LayerMask layerMask;

    private PlayerCombat _playerCombat;

    private List<Projectile> _rocks = new List<Projectile>();
    private List<PlayerController> _hitPlayer = new List<PlayerController>();

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        _hitPlayer.Clear();
    }

    public void Crack()
    {

        RaycastHit main;
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out main, _range, layerMask))
        {
            StartCoroutine(CastRays(main.distance));
            return;
        }
        StartCoroutine(CastRays(_range));
    }

    private IEnumerator CastRays(float distance)
    {
        for (float i = 0; i < distance; i += _betweenRayRange)
        {
            RaycastHit hit;
            Transform _spawnRay = _spawnPosition[0];

            if (Physics.Raycast(_spawnRay.position + _spawnRay.forward * i, -_spawnRay.up, out hit, _downwardRayRange))
            {
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    AlreadyHit(player);
                }
                else
                {
                    Projectile rock = Instantiate(_rock, hit.point, Random.rotation);
                    _rocks.Add(rock);
                    MeshRenderer mesh = hit.collider.GetComponent<MeshRenderer>();
                    if (rock._meshRenderer != null && mesh != null)
                    {
                        rock._meshRenderer.material = mesh.material;
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        Invoke("DestroyRocks", _rockDuration);
    }

    private void AlreadyHit(PlayerController player)
    {
        for (int i = 0; i < _hitPlayer.Count; i++)
        {
            if (player == _hitPlayer[i])
            {
                return;
            }
        }
        player.TakeDamage(_damage);
        _hitPlayer.Add(player);
    }


    private void DestroyRocks()
    {
        for (int i = 0; i < _rocks.Count; i++)
        {
            Destroy(_rocks[i].gameObject);
        }
        _rocks.Clear();
    }
}
