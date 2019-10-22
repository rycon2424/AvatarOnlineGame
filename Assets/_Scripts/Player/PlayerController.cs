using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : UsingOnline
{
    [SerializeField]
    private float _respawnTime;

    [SerializeField]
    private Transform _healthBarCanvas;

    private Animator anim;
    private Transform playerCamera;
    private PlayerUI pu;

    [Header("PlayerStats")]
    public Teams currentTeam;
    public enum Teams { noTeam, TeamRed, TeamBlue}
    public int _health;
    public bool _isAlive = true;

    [HideInInspector]
    public int _maxHealth;
    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public float _damageReduction = 1;

    public bool rotateTowardsCamera;

    void Start()
    {
        _maxHealth = _health;
        anim = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<OrbitCamera>().transform;
        pu = GetComponent<PlayerUI>();
        rotateTowardsCamera = true;
        pu.StartUI();

        //De if statement om te checken of jij de controle hebt over dat character
        if (pv.IsMine == false)
        {
            _healthBarCanvas.gameObject.SetActive(true);
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCamera.GetComponent<Camera>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
    }

    public void AssignTeam(int team)
    {
        pv.RPC("SyncMyTeam", RpcTarget.All, team);
    }

    [PunRPC]
    void SyncMyTeam(int team)
    {
        switch (team)
        {
            case 0:
                currentTeam = Teams.noTeam;
                break;
            case 1:
                currentTeam = Teams.TeamRed;
                break;
            case 2:
                currentTeam = Teams.TeamBlue;
                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        if (rotateTowardsCamera)
        {
            RotateToLook();
        }
            HealthBar();
        if (pv.IsMine == false)
        {
            return;
        }
        if (canMove)
        {
            Movement();
        }
    }

    private void HealthBar()
    {
        if (Camera.current != null)
        {
            _healthBarCanvas.LookAt(Camera.current.transform);
        }
    }

    public void TakeDamage(float damage)
    {
        damage /= _damageReduction;
        _health -= Mathf.RoundToInt(damage);
        if (pv.IsMine == true)
        {
            pv.RPC("SyncHealth", RpcTarget.All, _health);
        }
    }

    [PunRPC]
    void SyncHealth(int health)
    {
        _health = health;
        pu.UpdateHealth(_health);
        if (_health < 1)
        {
            Death();
        }
    }

    void Death()
    {
        rotateTowardsCamera = false;
        _isAlive = false;
        anim.Play("Death");
        Invoke("RespawnTime", _respawnTime);
    }

    void RespawnTime()
    {
        if (pv.IsMine == true)
        {
            pv.RPC("Respawn", RpcTarget.All);
        }
    }

    [PunRPC]
    void Respawn()
    {
        rotateTowardsCamera = true;
        _health = _maxHealth;
        pu.UpdateHealth(_health);
        if (pv.IsMine == true)
        {
            pv.RPC("SyncHealth", RpcTarget.All, _health);
        }
        anim.Play("Walk");
        _isAlive = true;
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