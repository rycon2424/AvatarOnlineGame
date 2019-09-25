using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCombat : UsingOnline
{
    [SerializeField]
    private Transform _cameraTransform;

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

    public Vector3 GetDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, _raycastRange, LayerMask))
        {
            Debug.DrawRay(_cameraTransform.position, hit.point, Color.red, 1);
            return hit.point;
        }
        return _cameraTransform.forward * _raycastRange;
    }

    public bool GroundTest(Transform drawrayPosition, Projectile projectile, float rayRange)
    {
        RaycastHit hit;
        if (Physics.Raycast(drawrayPosition.position, -drawrayPosition.up, out hit, rayRange))
        {
            if (projectile._meshRenderer != null)
            {
                projectile._meshRenderer.material = hit.collider.GetComponent<MeshRenderer>().material;
            }
            return true;
        }
        return false;
    }

    [PunRPC]
    void LightAttack()
    {
        if (_lightAttack._isReady)
        {
            _animator.Play("LightAttack");
            _lightAttack.UseMove(this);
        }
    }
    
    [PunRPC]
    void HeavyAttack()
    {
        if (_heavyAttack._isReady)
        {
            _animator.Play("HeavyAttack");
            _heavyAttack.UseMove(this);
        }
    }
    
    public void Ultimate()
    {
        if (_ultimate._isReady)
        {
            _ultimate.UseMove(this);
        }
    }
    
    public void Shield()
    {
        if (_shield._isReady)
        {
            _shield.UseMove(this);
        }
    }
}
