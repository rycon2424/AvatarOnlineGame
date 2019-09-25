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

    protected Animator _animator;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            pv.RPC("HeavyAttack", RpcTarget.All);
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
    void LightAttack()
    {
        if (_lightAttack._isReady)
        {
            _animator.Play("LightAttack");
            _lightAttack.UseMove(GetDirection());
        }
    }
    
    [PunRPC]
    void HeavyAttack()
    {
        if (_heavyAttack._isReady)
        {
            _animator.Play("HeavyAttack");
            _heavyAttack.UseMove(GetDirection());
        }
    }
    
    public void Ultimate()
    {
        if (_ultimate._isReady)
        {
            _ultimate.UseMove(GetDirection());
        }
    }
    
    public void Shield()
    {
        if (_shield._isReady)
        {
            _shield.UseMove(GetDirection());
        }
    }
}
