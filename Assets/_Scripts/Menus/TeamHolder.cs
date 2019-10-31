using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamHolder : MonoBehaviour
{
    [HideInInspector]
    public PlayerController.Teams _team;

    public Transform spawn;

    [SerializeField]
    private Text _nameText;

    public void SetTeamText()
    {
        _nameText.text = _team.ToString();
    }
}
