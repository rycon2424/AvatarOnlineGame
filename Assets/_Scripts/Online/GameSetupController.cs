using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("Playercreated");
        //PhotonNetwork.Instantiate(Path.Combine("Fire", "FireBender"), Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate(Path.Combine("Earth", "EarthBender"), Vector3.zero, Quaternion.identity);
    }
}