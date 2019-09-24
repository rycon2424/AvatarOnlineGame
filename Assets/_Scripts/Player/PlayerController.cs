using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : UsingOnline
{

    private Animator anim;
    private Transform playerCamera;
    private int _health;

    [HideInInspector]
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

        gameObject.layer = 9;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCamera.GetComponent<Camera>().enabled = true;
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

    public void ToggleRotation()
    {
        rotateTowardsCamera = !rotateTowardsCamera;
    }

    void RotateToLook()
    {
        var CharacterRotation = playerCamera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, CharacterRotation, Time.deltaTime * 12);
    }

}