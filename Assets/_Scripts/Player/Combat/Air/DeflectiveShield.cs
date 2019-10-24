using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectiveShield : Shield
{
    [SerializeField]
    protected Animator _animator;

    [SerializeField]
    protected Transform _shieldPosition;

    [SerializeField]
    protected Deflector _shield;

    [SerializeField]
    protected float _lerpSpeed;

    private bool _lerp;
    private Deflector _spawnedShield;
    private PlayerCombat _playerCombat;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        Invoke("EndDeflecting", _shieldDuration);
    }

    public IEnumerator LerpShield()
    {
        _spawnedShield = Instantiate(_shield, _shieldPosition.position, _shieldPosition.rotation);
        _spawnedShield._deflectiveShield = this;
        _lerp = true;

        while (_lerp && _spawnedShield != null)
        {
            _spawnedShield.transform.position = Vector3.Lerp(_spawnedShield.transform.position, _shieldPosition.position, _lerpSpeed * Time.deltaTime);
            _spawnedShield.transform.rotation = Quaternion.Lerp(_spawnedShield.transform.rotation, _shieldPosition.rotation, _lerpSpeed);
            yield return new WaitForEndOfFrame();

            if (!_playerCombat._playerController._isAlive)
            {
                _lerp = false;
                Destroy(_spawnedShield.gameObject);
            }
        }
    }

    private void EndDeflecting()
    {
        _lerp = false;
        Destroy(_spawnedShield.gameObject);
        _animator.Play("ShieldEnd");
    }

    public void DeflectProjectile(Projectile projectile)
    {
        projectile._owner = _playerCombat._playerController;
        projectile.transform.LookAt(_playerCombat.GetDirection());
    }
}
