using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : UsingOnline
{
    [SerializeField]
    private float _respawnTime;

    private Animator anim;
    private Transform playerCamera;
    private PlayerUI pu;
    
    public int _health;

    [HideInInspector]
    public int _maxHealth;
    [HideInInspector]
    public bool canMove = true;

    public bool rotateTowardsCamera;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<OrbitCamera>().transform;
        pu = GetComponent<PlayerUI>();
        rotateTowardsCamera = true;
        pu.StartUI();

        //De if statement om te checken of jij de controle hebt over dat character
        if (pv.IsMine == false)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCamera.GetComponent<Camera>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
    }
    
    void Update()
    {
        if (rotateTowardsCamera)
        {
            RotateToLook();
        }
        if (pv.IsMine == false)
        {
            return;
        }
        if (canMove)
        {
            Movement();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        pu.UpdateHealth(_health);
        if (pv.IsMine == true)
        {
            pv.RPC("SyncHealth", RpcTarget.All, _health);
        }
    }

    [PunRPC]
    void SyncHealth(int health)
    {
        _health = health;
        if (_health < 1)
        {
            Death();
            Invoke("Respawn", _respawnTime);
        }
    }

    void Death()
    {
        anim.Play("Death");

    }

    void Respawn()
    {
        transform.position = Vector3.zero;
        _health = _maxHealth;
        pv.RPC("SyncHealth", RpcTarget.All, _health);
        anim.Play("Walk");
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }
    
    void RotateToLook()
    {
        var CharacterRotation = playerCamera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        transform.rotation = CharacterRotation;
    }

}