using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : Shield
{
    [SerializeField]
    protected Transform _spawnPosition;

    [SerializeField]
    protected Projectile _wall;

    [SerializeField]
    protected float _rayRange;

    [SerializeField]
    protected float _lerpSpeed;

    private Projectile _spawnedWall;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _spawnedWall = Instantiate(_wall, new Vector3(_spawnPosition.position.x, _spawnPosition.position.y - _rayRange, _spawnPosition.position.z), _spawnPosition.rotation);
        if (!playerCombat.GroundTest(_spawnPosition, _spawnedWall, _rayRange))
        {
            Destroy(_spawnedWall.gameObject);
            return;
        }

        Invoke("DestroyWall", _shieldDuration);
    }

    public void DestroyWall()
    {
        if (_spawnedWall != null)
        {
            _spawnedWall.Destroy();
        }
    }

    public IEnumerator LerpWall()
    {
        Vector3 lerpPosition = _spawnPosition.position;

        while (_spawnedWall != null && Vector3.Distance(_spawnedWall.transform.position, lerpPosition) > 0.25)
        {
            Debug.Log("Lerp");
            yield return new WaitForEndOfFrame();
            _spawnedWall.transform.position = Vector3.Lerp(_spawnedWall.transform.position, lerpPosition, _lerpSpeed * Time.deltaTime);
        }
    }
}
