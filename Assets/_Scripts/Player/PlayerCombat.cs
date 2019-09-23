using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCombat : UsingOnline
{
    [SerializeField]
    private GameObject _cube;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (pv.IsMine == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            pv.RPC("Spawn", RpcTarget.All);
        }
    }

    [PunRPC]
    void Spawn()
    {
        GameObject cube = Instantiate(_cube, transform.position, transform.rotation);
        cube.transform.position += new Vector3(0, 3, 0);
    }
}
