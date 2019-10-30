using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour
{
    [Header("SpawnPoints")]
    [SerializeField]
    private List<Transform> _spawnPointsFFA;

    [SerializeField]
    private List<Transform> _spawnPointsTB1;

    [SerializeField]
    private List<Transform> _spawnPointsTB2;

    [Header("FreeForAll")]

    [Header("TeamDeathMatch")]


    [Header("ControlPoint")]
    [SerializeField]
    private int _matchDurationCP;

    [SerializeField]
    private int _matchPointsCP;

    [SerializeField]
    private Canvas _canvasCP;

    [SerializeField]
    private List<CapturePoint> _capturePoints;

    [SerializeField]
    private List<Text> _objectiveText;

    [SerializeField]
    private List<Image> _objectiveImage;

    [SerializeField]
    private List<Slider> _objectiveSlider;

    [SerializeField]
    private List<Text> _TeamPointText;

    [SerializeField]
    private List<Slider> _TeamPointSlider;

    private NationSelect _nationSelect;

    private List<int> _amountOfPlayes = new List<int>();

    public static GameModeManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartGamemode();
        for (int i = 0; i < GameModeEnum.GetNames(typeof(GameModeEnum)).Length; i++)
        {
            _amountOfPlayes.Add(0);
        }
    }

    public void StartGamemode()
    {

        switch (NationSelect.instance.gameMode)
        {
            case GameModeEnum.freeForAll:
                FreeForAll();
                break;
            case GameModeEnum.teamDeathMatch:
                TeamDeathMatch();
                break;
            case GameModeEnum.controlPoint:
                ControlPoint();
                break;
        }
    }

    public Vector3 FindSpawnPosition(PlayerController.Teams team)
    {
        switch (NationSelect.instance.gameMode)
        {
            case GameModeEnum.freeForAll:
                return _spawnPointsFFA[Random.Range(0, _spawnPointsFFA.Count)].position;

            case GameModeEnum.teamDeathMatch:
            case GameModeEnum.controlPoint:
                switch (team)
                {
                    case PlayerController.Teams.TeamRed:
                        return _spawnPointsTB1[Random.Range(0, _spawnPointsTB1.Count)].position;

                    case PlayerController.Teams.TeamBlue:
                        return _spawnPointsTB2[Random.Range(0, _spawnPointsTB2.Count)].position;
                }
                break;
        }

        return Vector3.zero;
    }

    private void FreeForAll()
    {

    }

    private void TeamDeathMatch()
    {

    }

    private void ControlPoint()
    {
        for (int i = 0; i < _TeamPointSlider.Count; i++)
        {
            _TeamPointSlider[i].maxValue = _matchPointsCP;
            _TeamPointText[i].text = "0";
        }

        for (int i = 0; i < _capturePoints.Count; i++)
        {
            _capturePoints[i].gameObject.SetActive(true);
        }

        StartCoroutine(ControlPointInProgress());
        StartCoroutine(Timer(_matchDurationCP));
    }

    private IEnumerator ControlPointInProgress()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < _capturePoints.Count; i++)
            {
                _objectiveText[i].text = "Point " + _capturePoints[i]._name + "\n" + _capturePoints[i].currentState.ToString();
                switch (_capturePoints[i].currentTeam)
                {
                    case PlayerController.Teams.noTeam:
                        _objectiveImage[i].color = Color.white;
                        break;
                    case PlayerController.Teams.TeamRed:
                        _objectiveImage[i].color = Color.red;
                        break;
                    case PlayerController.Teams.TeamBlue:
                        _objectiveImage[i].color = Color.blue;
                        break;
                }

                if (_capturePoints[i].currentState == CapturePoint.CaptureState.capturing)
                {
                    _objectiveSlider[i].gameObject.SetActive(true);
                    _objectiveSlider[i].maxValue = _capturePoints[i].CaptureTime;
                    _objectiveSlider[i].value = _capturePoints[i].progress;
                }
                else
                {
                    _objectiveSlider[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator Timer(int time)
    {
        for (int TimeLeft = time; TimeLeft > 0; TimeLeft--)
        {
            Debug.Log(TimeLeft + " sec left");
            yield return new WaitForSeconds(1);
            ManagePoints();
        }
    }

    private void ManagePoints()
    {
        switch (NationSelect.instance.gameMode)
        {
            case GameModeEnum.controlPoint:
                for (int i = 0; i < _capturePoints.Count; i++)
                {
                    if (_capturePoints[i].currentState == CapturePoint.CaptureState.captured || _capturePoints[i].currentState == CapturePoint.CaptureState.boosted)
                    {
                        if (_capturePoints[i].currentState == CapturePoint.CaptureState.boosted)
                        {
                            _amountOfPlayes[(int)_capturePoints[i].currentTeam]++;
                        }
                        _amountOfPlayes[(int)_capturePoints[i].currentTeam]++;
                        _TeamPointText[(int)_capturePoints[i].currentTeam - 1].text = _amountOfPlayes[(int)_capturePoints[i].currentTeam].ToString();
                        _TeamPointSlider[(int)_capturePoints[i].currentTeam - 1].value = _amountOfPlayes[(int)_capturePoints[i].currentTeam];

                    }
                }
                return;
        }
    }
}

