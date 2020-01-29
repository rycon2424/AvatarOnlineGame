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

    private CharacterController characterController;

    private List<PlayerController> _hitBy = new List<PlayerController>();

    void Start()
    {
        _maxHealth = _health;
        anim = GetComponent<Animator>();
        if (GetComponentInChildren<OrbitCamera>() != null)
        {
            playerCamera = GetComponentInChildren<OrbitCamera>().transform;
        }
        characterController = GetComponent<CharacterController>();
        pu = GetComponent<PlayerUI>();
        rotateTowardsCamera = true;
        pu.StartUI();
        if (ScoreBoard.Instance != null)
        {
            ScoreBoard.Instance.AddPlayer(this);
        }

        //De if statement om te checken of jij de controle hebt over dat character

            if (pv != null && pv.IsMine == false)
            {
                _healthBarCanvas.gameObject.SetActive(true);
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera != null)
        {
            playerCamera.GetComponent<Camera>().enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;
        }
    }

    public void AssignTeam(Teams owner)
    {
        pv.RPC("SyncMyTeam", RpcTarget.AllBuffered, owner);
    }

    [PunRPC]
    void SyncMyTeam(Teams team)
    {
        currentTeam = team;
    }
    
    void Update()
    {

        if (rotateTowardsCamera)
        {
            RotateToLook();
        }
            HealthBar();
        if (pv == null || pv.IsMine == false)
        {
            return;
        }
        if (canMove)
        {
            Movement();
        }

        //debuging
        if (Input.GetKeyDown(KeyCode.L))
        {
            pv.RPC("SyncHealth", RpcTarget.All, 0);
        }
    }

    private void HealthBar()
    {
        if (Camera.current != null)
        {
            _healthBarCanvas.LookAt(Camera.current.transform);
        }
    }

    public void TakeDamage(float damage, PlayerController player)
    {
        if (player.currentTeam == currentTeam)
        {
            if (player.currentTeam != Teams.noTeam)
            {
                return;
            }
        }

        Debug.Log("_isAlive " + _isAlive);
        if (pv == null || pv.IsMine == true)
        {

            damage /= _damageReduction;
            _health -= Mathf.RoundToInt(damage);
            AddHitBy(player);

            if (_health < 1)
            {
                if (ScoreBoard.Instance != null)
                {
                    ScoreBoard.Instance.AddValues(player, this, _hitBy);
                    _hitBy.Clear();
                }
                if (KillFeed.killfeedInstance != null)
                {
                    KillFeed.killfeedInstance.UpdateBattleLog("killed", player.pv.Owner.NickName, pv.Owner.NickName);
                }
            }

            if (pv != null)
            {
                pv.RPC("SyncHealth", RpcTarget.All, _health);
                return;
            }
            SyncHealth(_health);
        }
    }

    private void AddHitBy(PlayerController player)
    {
        for (int i = 0; i < _hitBy.Count; i++)
        {
            if (_hitBy[i] == player)
            {
                return;
            }
        }
        _hitBy.Add(player);
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
        characterController.detectCollisions = false;
        anim.Play("Death");
        StartCoroutine(RespawnTime());
    }

    private IEnumerator RespawnTime()
    {
        if (pv != null && pv.IsMine == true)
        {
            yield return new WaitForSeconds(_respawnTime - 1);
            anim.applyRootMotion = false;
            transform.position = GameModeManager.instance.FindSpawnPosition(currentTeam);
            yield return new WaitForSeconds(1);
            pv.RPC("Respawn", RpcTarget.All);
        }
    }

    [PunRPC]
    void Respawn()
    {
        anim.rootPosition = Vector3.zero;
        anim.applyRootMotion = true;
        characterController.detectCollisions = true;

        rotateTowardsCamera = true;
        _health = _maxHealth;
        pu.UpdateHealth(_health);
        if (pv != null && pv.IsMine == true)
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
        if (playerCamera != null)
        {
            var CharacterRotation = playerCamera.transform.rotation;
            CharacterRotation.x = 0;
            CharacterRotation.z = 0;
            transform.rotation = CharacterRotation;
        }
    }

}