﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Door photon.pun the gebruiken en monobehaviourpun te gebruiken kunnen we callbacks en functions aanroepen die gesynced kunnen worden
/// </summary>
public class UsingOnline : MonoBehaviourPun
{
    [HideInInspector]
    public PhotonView pv;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (pv == null)
        {
            pv = GetComponentInParent<PhotonView>();
        }
    }

}