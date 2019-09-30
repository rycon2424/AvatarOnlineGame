using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GroundSlam : Ranged
{
    private List<PlayerController> _players = new List<PlayerController>();

    [SerializeField]
    private int StoneCount;

    [SerializeField]
    protected float _downwardRayRange;

    [SerializeField]
    protected Projectile _rock;

    [SerializeField]
    protected float _rockDuration;

    [SerializeField]
    protected LayerMask _layerMask;

    private PlayerCombat _playerCombat;

    private List<Projectile> _rocks = new List<Projectile>();

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    public void Slam()
    {
        _players.AddRange(FindObjectsOfType<PlayerController>());
        CastRays();
        for (int i = 0; i < _players.Count; i++)
        {
            if (_playerCombat._playerController != _players[i] && Vector3.Distance(transform.position, _players[i].transform.position) < _range)
            {
                _players[i].TakeDamage(_damage);
            }
        }
    }

    private void CastRays()
    {

        float angle = 0;
        for (int i = 0; i < StoneCount; i++)
        {
            float x = Mathf.Sin(angle) *_range;
            float z = Mathf.Cos(angle) * _range;
            angle += 2 * Mathf.PI / StoneCount;

            Vector3 direction = new Vector3(_spawnPosition[0].position.x + x,_spawnPosition[0].position.y, _spawnPosition[0].position.z + z);
            RaycastHit hit;
            Debug.DrawRay(direction, -transform.up, Color.red,5);
            if (Physics.Raycast(direction, -transform.up, out hit, _downwardRayRange, _layerMask))
            {
                Projectile rock = Instantiate(_rock, hit.point, Random.rotation);
                _rocks.Add(rock);
                if (rock._meshRenderer != null)
                {
                    rock._meshRenderer.material = hit.collider.GetComponent<MeshRenderer>().material;
                }
            }
        }

        Invoke("DestroyRocks", _rockDuration);
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
       
