using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : UsingOnline
{

    private Animator anim;
    private Transform playerCamera;

    [SerializeField]
    private int _health;
    
    public bool rotateTowardsCamera;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<OrbitCamera>().transform;
        rotateTowardsCamera = true;

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
        if (pv.IsMine == false)
        {
            return;
        }
        Movement();
        if (rotateTowardsCamera)
        {
            RotateToLook();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        pv.RPC("SyncHP", RpcTarget.All, _health);
    }

    [PunRPC]
    void SyncHP(int hp)
    {
        _health = hp;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, CharacterRotation, Time.deltaTime * 12);
    }

}