using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [HideInInspector]
    public PlayerController _player;
    [HideInInspector]
    public int _kills = 0;
    [HideInInspector]
    public int _assits = 0;
    [HideInInspector]
    public int _deaths = 0;

    [Header("UI")]
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _killsText;
    [SerializeField]
    private Text _assitsText;
    [SerializeField]
    private Text _deathsText;

    public void UpdateUI()
    {
        _nameText.text = _player.pv.Owner.NickName;
        _killsText.text = _kills.ToString();
        _assitsText.text = _assits.ToString();
        _deathsText.text = _deaths.ToString();
    }
}
