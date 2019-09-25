using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCombat : UsingOnline
{
    [SerializeField]
    private Transform _cameraTarnsform;

    [SerializeField]
    private Moves _lightAttack;

    [SerializeField]
    private Moves _heavyAttack;

    [SerializeField]
    private Moves _ultimate;

    [SerializeField]
    private Moves _shield;

    [SerializeField]
    private LayerMask LayerMask;

    private float _raycastRange = 100;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LightAttack();
        }
    }

    private Vector3 GetDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(_cameraTarnsform.position, _cameraTarnsform.forward, out hit, _raycastRange, LayerMask))
        {
            Debug.DrawRay(_cameraTarnsform.position, hit.point, Color.red, 1);
            return hit.point;
        }
        return _cameraTarnsform.forward * _raycastRange;
    }

    /// <summary>
    /// LIGHT ATTACK
    /// </summary>
    public void LightAttack()
    {
        pv.RPC("SyncedLightAttack", RpcTarget.All);
    }

    [PunRPC]
    private void SyncedLightAttack()
    {
        if (_lightAttack._isReady)
        {
            _lightAttack.UseMove(GetDirection());
        }
    }

    /// <summary>
    /// HEAVY ATTACK
    /// </summary>
    public void HeavyAttack()
    {
        pv.RPC("SyncedHeavyAttack", RpcTarget.All);
    }

    [PunRPC]
    private void SyncedHeavyAttack()
    {
        if (_heavyAttack._isReady)
        {
            _heavyAttack.UseMove(GetDirection());
        }
    }

    /// <summary>
    /// ULTIMATE ATTACK
    /// </summary>
    public void Ultimate()
    {
        if (_ultimate._isReady)
        {
            _ultimate.UseMove(GetDirection());
        }
    }

    /// <summary>
    /// SHIELD ABILITY
    /// </summary>
    public void Shield()
    {
        if (_shield._isReady)
        {
            _shield.UseMove(GetDirection());
        }
    }
}
