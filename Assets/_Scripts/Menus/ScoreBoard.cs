using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ScoreBoard : MonoBehaviourPun, IPunObservable
{
    public static ScoreBoard Instance;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private PlayerScore _playerScorePrefab;

    [SerializeField]
    private GridLayoutGroup _gridLayout;

    [SerializeField]
    private TeamHolder _holderPrefab;

    private List<TeamHolder> _teams = new List<TeamHolder>();

    private List<PlayerScore> _playerScores = new List<PlayerScore>();


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        for (int i = 0; i < _playerScores.Count; i++)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_playerScores[i]._kills);
                stream.SendNext(_playerScores[i]._assits);
                stream.SendNext(_playerScores[i]._deaths);
            }
            else
            {
                _playerScores[i]._kills = (int)stream.ReceiveNext();
                _playerScores[i]._assits = (int)stream.ReceiveNext();
                _playerScores[i]._deaths = (int)stream.ReceiveNext();
                _playerScores[i].UpdateUI();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        _canvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _canvas.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _canvas.enabled = false;
        }
    }

    public void AddPlayer(PlayerController player)
    {
        for (int i = 0; i < _teams.Count; i++)
        {
            if (_teams[i]._team == player.currentTeam)
            {
                PlayerScore playerScore = Instantiate(_playerScorePrefab, _teams[i].spawn);
                _gridLayout.spacing = new Vector2(0, _gridLayout.cellSize.y * _playerScores.Count);
                playerScore._player = player;
                playerScore.UpdateUI();
                _playerScores.Add(playerScore);
                return;
            }
        }

        TeamHolder teamHolder = Instantiate(_holderPrefab, gameObject.transform);
        teamHolder._team = player.currentTeam;
        teamHolder.SetTeamText();
        _teams.Add(teamHolder);
        AddPlayer(player);
    }

    public void AddValues(PlayerController kill, PlayerController death, List<PlayerController> assists)
    {
        for (int i = 0; i < _playerScores.Count; i++)
        {
            if (_playerScores[i]._player == kill)
            {
                _playerScores[i]._kills++;
                _playerScores[i].UpdateUI();
            }

            if (_playerScores[i]._player == death)
            {
                _playerScores[i]._deaths++;
                _playerScores[i].UpdateUI();
            }

            for (int j = 0; j < assists.Count; j++)
            {
                if (_playerScores[i]._player == assists[j] && assists[j] != kill)
                {
                    _playerScores[i]._assits++;
                    _playerScores[i].UpdateUI();
                }
            }
        }
    }
}
