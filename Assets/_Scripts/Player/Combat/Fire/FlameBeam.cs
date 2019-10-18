using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBeam : RaycastBased
{

    public GameObject firebeam;
    public GameObject playerCamera;

    private OrbitCamera ob;
    private float originalSensitivity;

    private PlayerCombat _playerCombat;
    public override void UseMove(PlayerCombat playerCombat)
    {
        base.UseMove(playerCombat);
        _playerCombat = playerCombat;
    }

    void Start()
    {
        ob = playerCamera.GetComponent<OrbitCamera>();
    }
    
    void Update()
    {
        firebeam.transform.rotation = playerCamera.transform.rotation;
        if (!_playerCombat._playerController._isAlive)
        {
            DisableBeam();
        }
    }

    public void EnableBeam()
    {
        firebeam.SetActive(true);
        originalSensitivity = ob.sensitivity;
        ob.sensitivity = ob.sensitivity / 3;
    }

    public void DisableBeam()
    {
        firebeam.SetActive(false);
        playerCamera.GetComponent<OrbitCamera>().sensitivity = 75;
        ob.sensitivity = originalSensitivity;
    }

    public void DamageTick()
    {
        RaycastHit hit;
        Debug.DrawRay(_spawnPosition[0].position, _spawnPosition[0].forward * _range, Color.red, 3);
        if (Physics.Raycast(_spawnPosition[0].position, _spawnPosition[0].forward, out hit, _range))
        {
            PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(_damage);
                return;
            }
        }
    }

}
