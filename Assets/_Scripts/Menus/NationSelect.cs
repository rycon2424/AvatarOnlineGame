﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NationSelect : MonoBehaviourPun
{
    private PlayerController player;

    [SerializeField]
    private GameModeManager _gameModeManager;

    [Header("GameMode")]
    public GameModeEnum gameMode;

    [Header("Player Selection")]
    public PlayerController.Teams currentSelectedTeam;

    [Header("Canvas References")]
    public GameObject teamPickScreen;
    public GameObject charPickScreen;

    public static NationSelect instance;

    public PhotonView pvc;

    void Awake()
    {
        gameMode = CustomMatchmakingRoomController.instanceGamemode._currentGamemode;
        if (PhotonNetwork.IsMasterClient == true)
        {
            pvc.RPC("SyncGameMode", RpcTarget.AllBuffered, gameMode);
        }
        Destroy(CustomMatchmakingRoomController.instanceGamemode.gameObject);
    }
    
    [PunRPC]
    void SyncGameMode(GameModeEnum gm)
    {
        gameMode = gm;
        StartSelect();
    }

    void StartSelect()
    {
        instance = this;
        switch (gameMode)
        {
            case GameModeEnum.freeForAll:
                SwitchTeam(0);
                EnableCharacterSelection();
                break;
            case GameModeEnum.teamDeathMatch:
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
        switch (team)
        {
            case 0:
                currentSelectedTeam = PlayerController.Teams.noTeam;
                break;
            case 1:
                currentSelectedTeam = PlayerController.Teams.TeamRed;
                break;
            case 2:
                currentSelectedTeam = PlayerController.Teams.TeamBlue;
                break;
            default:
                break;
        }
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
    public void CreatePlayer(string currentSelectedPlayer)
    {
        string[] words = currentSelectedPlayer.Split(' ');
        playerRef = PhotonNetwork.Instantiate(Path.Combine(words[0], words[1]), _gameModeManager.FindSpawnPosition(currentSelectedTeam), Quaternion.identity);
        PlayerController pc = playerRef.GetComponent<PlayerController>();
        pc.AssignTeam(currentSelectedTeam);
        charPickScreen.SetActive(false);
    }

}
