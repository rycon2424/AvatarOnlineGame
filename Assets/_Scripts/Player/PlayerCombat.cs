﻿using System.Collections;
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
        if (pv.IsMine == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pv.RPC("LightAttack", RpcTarget.All);
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
    
    [PunRPC]
    public void LightAttack()
    {
        if (_lightAttack._isReady)
        {
            _lightAttack.UseMove(GetDirection());
        }
    }
    
    [PunRPC]
    void HeavyAttack()
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
