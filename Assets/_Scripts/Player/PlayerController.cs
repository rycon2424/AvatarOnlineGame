using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : UsingOnline
{

    private Animator anim;
    private Transform playerCamera;
    private PlayerUI pu;
    
    public int _health;

    [HideInInspector]
    public int _maxHealth;
    
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
        Movement();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        pu.UpdateHealth(_health);
        //pv.RPC("SyncHP", RpcTarget.All, _health);
    }

    /*[PunRPC]
    void SyncHP(int hp)
    {
        _health = hp;
    }*/

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