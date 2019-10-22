using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCrush : RaycastBased
{
    [SerializeField]
    private Projectile _crusher;
    [SerializeField]
    private float _lerpSpeed;
    [SerializeField]
    private float _closeSpeed;
    [SerializeField]
    private float _distanceBetweenRocks;
    [SerializeField]
    private float _spawnDept;
    [SerializeField]
    private Transform _center;

    private PlayerCombat _playerCombat;
    private Projectile _spawnedCrusherL;
    private Projectile _spawnedCrusherR;
    private bool _lerp;

    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
        _center.position = _spawnPosition[0].position;
        _lerp = true;
    }

    public void Starting()
    {
        RaycastHit hit;
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            _center.position = hit.point;
        }
        else
        {
            _center.position += (_center.forward * _range);
        }
        _playerCombat._playerController.rotateTowardsCamera = false;
        _spawnedCrusherL = Instantiate(_crusher, (_center.position + -_center.right * _distanceBetweenRocks / 2) - _center.up * _spawnDept, transform.rotation);
        _spawnedCrusherR = Instantiate(_crusher, (_center.position + _center.right * _distanceBetweenRocks / 2) - _center.up * _spawnDept, transform.rotation);
        _spawnedCrusherL.transform.LookAt(_spawnedCrusherR.transform.position);
        _spawnedCrusherR.transform.LookAt(_spawnedCrusherL.transform.position);

        StartCoroutine(LerpUp());
    }

    private IEnumerator LerpUp()
    {

        while (_lerp && _spawnedCrusherL != null)
        {
            _spawnedCrusherL.transform.position = Vector3.Lerp(_spawnedCrusherL.transform.position, _center.position + -_center.right * _distanceBetweenRocks / 2, _lerpSpeed * Time.deltaTime);
            _spawnedCrusherR.transform.position = Vector3.Lerp(_spawnedCrusherR.transform.position, _center.position + _center.right * _distanceBetweenRocks / 2, _lerpSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();

            if (!_playerCombat._playerController._isAlive)
            {
                Destroy(_spawnedCrusherL.gameObject);
                Destroy(_spawnedCrusherR.gameObject);
            }
        }
    }

    public void Crush()
    {
        _lerp = false;
        _playerCombat._playerController.rotateTowardsCamera = true;
        _spawnedCrusherL.Fired(_damage, _closeSpeed, _distanceBetweenRocks/2 , _playerCombat._playerController.currentTeam);
        _spawnedCrusherR.Fired(_damage, _closeSpeed, _distanceBetweenRocks/2 , _playerCombat._playerController.currentTeam);
    }
}
