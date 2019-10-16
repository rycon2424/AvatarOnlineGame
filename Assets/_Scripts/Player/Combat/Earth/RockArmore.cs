using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockArmore : Shield
{
    [SerializeField]
    private Transform _center;

    [SerializeField]
    private float _damageReduction;

    [SerializeField]
    private Projectile _rocks;

    [SerializeField]
    private List<Transform> _rockPositions;

    [SerializeField]
    private float _grabRange;

    [SerializeField]
    private float _rayRange;
    
    [SerializeField]
    private float _lerpSpeed;

    [SerializeField]
    private float _lerpWalkSpeed;

    private float _currentLerpSpeed;
    private bool _lerp;
    private PlayerCombat _playerCombat;
    private List<Projectile> _projectiles = new List<Projectile>();

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        _currentLerpSpeed = _lerpSpeed;
        _lerp = true;
        Invoke("End", _shieldDuration);
        for (int i = 0; i < _rockPositions.Count; i++)
        {
            StartCoroutine(LerpRocks(_rockPositions[i]));
        }
    }
    private IEnumerator LerpRocks(Transform lerpPosition)
    {
        Transform spawnPos = _center;
        float x = Random.insideUnitCircle.x * _grabRange;
        float z = Random.insideUnitCircle.y * _grabRange;
        x += transform.position.x;
        z += transform.position.z;
        spawnPos.position = new Vector3(x, _center.position.y, z);
        Projectile _spawnedProjectile = Instantiate(_rocks, new Vector3(spawnPos.position.x, spawnPos.position.y - _rayRange, spawnPos.position.z), Random.rotation);
        if (!_playerCombat.GroundTest(spawnPos, _spawnedProjectile, _rayRange))
        {
            Destroy(_spawnedProjectile.gameObject);
        }
        else
        {
            _projectiles.Add(_spawnedProjectile);
            _playerCombat._playerController._damageReduction += _damageReduction;
        }

        while (_lerp && _spawnedProjectile != null)
        {
            _spawnedProjectile.transform.position = Vector3.Lerp(_spawnedProjectile.transform.position, lerpPosition.position, _currentLerpSpeed * Time.deltaTime);
            _spawnedProjectile.transform.rotation = Quaternion.Lerp(_spawnedProjectile.transform.rotation, lerpPosition.rotation, _currentLerpSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();

            if (!_playerCombat._playerController._isAlive)
            {
                End();
            }
        }
    }

    public override void DoneAnimating(AttackEnum attack)
    {
        base.DoneAnimating(attack);
        _currentLerpSpeed = _lerpWalkSpeed;
    }

    private void End()
    {
        _lerp = false;
        _playerCombat._playerController._damageReduction = 1;
        for (int i = 0; i < _projectiles.Count; i++)
        {
            _projectiles[i].Destroy();
        }
        _projectiles.Clear();
    }
}
