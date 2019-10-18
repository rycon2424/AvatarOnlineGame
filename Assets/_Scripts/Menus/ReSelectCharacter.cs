using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReSelectCharacter : UsingOnline
{

    private PlayerController pc;

    void Start()
    {
        pc = GetComponent<PlayerController>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReSelect();
        }
        if (pc._isAlive == false)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                ReSelect();
            }
        }
    }

    void ReSelect()
    {
        NationSelect.instance.EnableSelection();
        PhotonNetwork.Destroy(this.gameObject);
    }

}
