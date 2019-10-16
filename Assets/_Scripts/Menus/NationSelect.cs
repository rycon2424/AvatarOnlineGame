using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NationSelect : MonoBehaviourPun
{
    private PlayerController player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void CreatePlayer(int currentSelectedPlayer)
    {
        switch (currentSelectedPlayer)
        {
            case 0:
                PhotonNetwork.Instantiate(Path.Combine("Fire", "FireBender"), Vector3.zero, Quaternion.identity);
                break;
            case 1:
                PhotonNetwork.Instantiate(Path.Combine("Air", "AirBender"), Vector3.zero, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate(Path.Combine("Earth", "EarthBender"), Vector3.zero, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate(Path.Combine("Water", "WaterBender"), Vector3.zero, Quaternion.identity);
                break;
            case 4:
                PhotonNetwork.Instantiate(Path.Combine("Earth", "EarthBender2"), Vector3.zero, Quaternion.identity);
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
    }
}
