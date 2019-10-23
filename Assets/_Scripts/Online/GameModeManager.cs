using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField]
    private NationSelect _nationSelect;

    [SerializeField]
    private List<Transform> _spawnPointsFFA;

    [SerializeField]
    private List<Transform> _spawnPointsTB1;

    [SerializeField]
    private List<Transform> _spawnPointsTB2;

    //private List<Transform> _spawnPointsCurrent = new List<Transform>();

    private void Start()
    {

    }

    public Vector3 FindSpawnPosition(PlayerController.Teams team)
    {
        switch (_nationSelect.gameMode)
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
}
