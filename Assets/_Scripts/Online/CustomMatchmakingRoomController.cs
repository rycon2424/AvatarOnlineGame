using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    public GameModeEnum _currentGamemode;

    [SerializeField]
    private List<MapSettings> _maps;

    private MapSettings _selectedMap;

    [SerializeField]
    private Dropdown _mapDropDown;

    [SerializeField]
    private GameObject lobbyPanel; //display for when in lobby
    [SerializeField]
    private GameObject roomPanel; //display for when in room

    [SerializeField]
    private GameObject startButton; //only for the master client. used to start the game and load the multiplayer scene

    [SerializeField]
    private Transform playersContainer; //used to display all the players in the current room
    [SerializeField]
    private GameObject playerListingPrefab; //Instantiate to display each player in the room

    [SerializeField]
    private Text roomNameDisplay; //display for the name of the room

    [Header("Room Settings")]
    public bool canJoinInProgress; //display for the name of the room
    
    private void OnValidate()
    {
        for (int i = 0; i < _maps.Count; i++)
        {
            _maps[i].UpdateList();
        }
    }

    private void Start()
    {
        SelectedGamemode(_mapDropDown);
    }

    void ClearPlayerListings()
    {
        for (int i = playersContainer.childCount - 1; i >= 0; i--) //loop through all child object of the playersContainer, removing each child
        {
            Destroy(playersContainer.GetChild(i).gameObject);
        }
    }

    void ListPlayers()
    {

        foreach (Player player in PhotonNetwork.PlayerList) //loop through each player and create a player listing
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playersContainer);
            Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
            tempText.text = player.NickName;
        }

    }

    public override void OnJoinedRoom()//called when the local player joins the room
    {
        roomPanel.SetActive(true); //activate the display for being in a room
        lobbyPanel.SetActive(false); //hide the display for being in a lobby
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name; //update room name display
        if (PhotonNetwork.IsMasterClient) //if master client then activate the start button
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
        //photonPlayers = PhotonNetwork.PlayerList;
        ClearPlayerListings(); //remove all old player listings
        ListPlayers(); //relist all current player listings
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //called whenever a new player enter the room
    {
        ClearPlayerListings(); //remove all old player listings
        ListPlayers(); //relist all current player listings
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)//called whenever a player leave the room
    {
        ClearPlayerListings();//remove all old player listings
        ListPlayers();//relist all current player listings
        if (PhotonNetwork.IsMasterClient)//if the local player is now the new master client then we activate the start button
        {
            startButton.SetActive(true);
        }
    }

    public void StartGameOnClick() //paired to the start button. will load all players into the multiplayer scene through the master client and AutomaticallySyncScene
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (canJoinInProgress == false)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false; //Comment out if you want player to join after the game has started
            }
            if (_mapDropDown.value != 0)
            {
                Debug.Log("load: " + _selectedMap.SceneName + " " + _currentGamemode);
                PhotonNetwork.LoadLevel(_selectedMap.SceneName + " " + _currentGamemode);
            }
        }
    }

    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick() // paired to the back button in the room panel. will return the player to the lobby panel.
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }

    public void SelectedGamemode(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                _currentGamemode = GameModeEnum.freeForAll;
                break;
            case 1:
                _currentGamemode = GameModeEnum.teamDeathMatch;
                break;
            case 2:
                _currentGamemode = GameModeEnum.controlPoint;
                break;
        }
        _mapDropDown.options.Clear();
        _mapDropDown.captionText.text = "none";
            _mapDropDown.options.Add(new Dropdown.OptionData() { text = "none" });

        for (int i = 0; i < _maps.Count; i++)
        {
            if (_maps[i].selectGameMode[(int)_currentGamemode])
            {
                _mapDropDown.options.Add(new Dropdown.OptionData() { text = _maps[i].SceneName });
            }
        }
        _mapDropDown.value = 0;
        SelectedMap(_mapDropDown);
    }

    public void SelectedMap(Dropdown dropdown)
    {
        for (int i = 0; i < _maps.Count; i++)
        {
            if (dropdown.options[dropdown.value].text == _maps[i].SceneName)
            {
                _selectedMap = _maps[i];
            }
        }
    }
}
