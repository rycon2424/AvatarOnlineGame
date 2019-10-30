using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    public string _name;
    public float CaptureTime;
    public GameModeManager _gameModeManager;
    public PlayerController.Teams currentTeam;
    public CaptureState currentState;
    public enum CaptureState { neutral, capturing, captured, contested, boosted }

    public float progress = 0;

    [SerializeField]
    private int PlayersForBoosted;


    private List<PlayerController> _onPointPlayers = new List<PlayerController>();
    private List<int> _teams = new List<int>();

    private PlayerController.Teams owningTeam;
    private bool _hasBeenCaptured = false;


    private void Start()
    {
        for (int i = 0; i < GameModeEnum.GetNames(typeof(GameModeEnum)).Length; i++)
        {
            _teams.Add(0);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _onPointPlayers.Add(player);
            CalculateState(player, 1);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            _onPointPlayers.Remove(player);
            CalculateState(player, -1);
        }
    }

    private IEnumerator Capturing()
    {
        for (progress = 0; progress < CaptureTime;)
        {
            yield return new WaitForSeconds(0.1f);
            if (currentState == CaptureState.capturing)
            {
                progress += 0.1f;
            }
            else
            {
                progress = CaptureTime;
            }
        }
        if (currentState == CaptureState.capturing)
        {
            progress = 0;
            _hasBeenCaptured = true;
            owningTeam = currentTeam;

            currentState = CaptureState.captured;
            if (_teams[(int)currentTeam] >= PlayersForBoosted)
            {
                currentState = CaptureState.boosted;
            }
        }
    }

    private void CalculateState(PlayerController player, int point)
    {
        int teamsOnPoint = 0;
        _teams[(int)player.currentTeam] += point;
        for (int i = 0; i < _teams.Count; i++)
        {
            if (_teams[i] != 0)
            {
                teamsOnPoint++;
            }
        }

        for (int i = 0; i < _onPointPlayers.Count; i++)
        {
            if (_onPointPlayers[i] == null)
            {
                _onPointPlayers.Remove(_onPointPlayers[i]);
                for (int j = 0; j < _teams.Count; j++)
                {
                    _teams[j] = 0;
                    for (int k = 0; k < _onPointPlayers.Count; k++)
                    {
                        if ((int)_onPointPlayers[k].currentTeam == j)
                        {
                            j++;
                        }
                    }
                }
            }
        }

        if (teamsOnPoint > 1)
        {
            currentState = CaptureState.contested;
            return;
        }


        for (int i = 0; i < _teams.Count; i++)
        {
            if (_teams[i] != 0)
            {
                if ((int)currentTeam != i)
                {
                    PlayerController.Teams temp = (PlayerController.Teams)i;
                    currentTeam = temp;
                    if (currentState != CaptureState.capturing)
                    {
                        currentState = CaptureState.capturing;
                        StartCoroutine(Capturing());
                    }
                    return;
                }
                Debug.Log("PlayersOnPoint: " + _teams[i] + "PlayersForBoosted: " + PlayersForBoosted);

                if (_teams[i] >= PlayersForBoosted && currentState == CaptureState.captured)
                {
                    currentState = CaptureState.boosted;
                    return;
                }
                if (_teams[i] < PlayersForBoosted && currentState == CaptureState.boosted)
                {
                    currentState = CaptureState.captured;
                    return;
                }
            }
            else
            {
                if (currentState == CaptureState.capturing || currentState == CaptureState.contested)
                {
                    if (_hasBeenCaptured)
                    {
                        currentState = CaptureState.captured;
                        currentTeam = owningTeam;
                    }
                    else
                    {
                        currentState = CaptureState.neutral;
                        currentTeam = PlayerController.Teams.noTeam;
                    }
                }

                if ((int)currentTeam == i && _teams[i] < PlayersForBoosted && currentState == CaptureState.boosted)
                {
                    currentState = CaptureState.captured;
                    return;
                }
            }
        }
    }
}
