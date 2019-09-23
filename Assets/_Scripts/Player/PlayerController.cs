using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : UsingOnline
{

    private Animator anim;
    private Transform playerCamera;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<OrbitCamera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //De if statement om te checken of jij de controle hebt over dat character
        if (pv.IsMine == false)
        {
            return;
        }
        playerCamera.GetComponent<Camera>().enabled = true;
    }
    
    void Update()
    {
        if (pv.IsMine == false)
        {
            return;
        }
        Movement();
        RotateToLook();
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