using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NationSelect : MonoBehaviourPun
{
    private PlayerController player;

    [Header("GameMode")]
    public SelectGameMode gameMode;
    public enum SelectGameMode { freeForAll, teamBased }

    [Header("Player Selection")]
    public int currentSelectedTeam;

    [Header("Canvas References")]
    public GameObject teamPickScreen;
    public GameObject charPickScreen;

    public static NationSelect instance;

    void Start()
    {
        instance = this;
        switch (gameMode)
        {
            case SelectGameMode.freeForAll:
                SwitchTeam(0);
                EnableCharacterSelection();
                break;
            case SelectGameMode.teamBased:
                EnableTeamSelection();
                break;
            default:
                break;
        }
    }

    public void EnableTeamSelection()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        teamPickScreen.SetActive(true);
        charPickScreen.SetActive(false);
    }

    public void SwitchTeam(int team)
    {
        currentSelectedTeam = team;
        EnableCharacterSelection();
    }
    
    public void EnableCharacterSelection()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        teamPickScreen.SetActive(false);
        charPickScreen.SetActive(true);
    }

    GameObject playerRef;
    PhotonView pv;
    public void CreatePlayer(int currentSelectedPlayer)
    {
        switch (currentSelectedPlayer)
        {
            case 0:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Fire", "FireBender"), Vector3.zero, Quaternion.identity);
                break;
            case 1:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Air", "AirBender"), Vector3.zero, Quaternion.identity);
                break;
            case 2:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Earth", "EarthBender"), Vector3.zero, Quaternion.identity);
                break;
            case 3:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Water", "WaterBender"), Vector3.zero, Quaternion.identity);
                break;
            case 4:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Earth", "EarthBender2"), Vector3.zero, Quaternion.identity);
                break;
            case 5:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Fire", "FireBender2"), Vector3.zero, Quaternion.identity);
                break;
            case 6:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Water", "WaterBender2"), Vector3.zero, Quaternion.identity);
                break;
            case 7:
                playerRef = PhotonNetwork.Instantiate(Path.Combine("Air", "AirBender2"), Vector3.zero, Quaternion.identity);
                break;
            default:
                break;
        }
        PlayerController pc = playerRef.GetComponent<PlayerController>();
        pc.StartPlayer(currentSelectedTeam);
        charPickScreen.SetActive(false);
    }

}
