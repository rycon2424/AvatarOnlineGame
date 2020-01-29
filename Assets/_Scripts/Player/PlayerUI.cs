using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject UI;

    [SerializeField]
    private Text _playerName;

    [SerializeField]
    private List<Image> _images;

    [SerializeField]
    private List<Text> _timerText;

    public Slider healthBar;

    private PlayerController pc;

    public void StartUI()
    {
        pc = GetComponent<PlayerController>();
        healthBar.value = pc._maxHealth;
        healthBar.maxValue = pc._maxHealth;
        if (pc.pv == null)
        {
            _playerName.text = "TestDummy";

        }
        else
        {
            _playerName.text = pc.pv.Owner.NickName;
        }
        UpdateHealth(pc._health);
        Color teamColor = Color.red;
        switch (pc.currentTeam)
        {
            case PlayerController.Teams.noTeam:
                teamColor = Color.red;
                break;
            case PlayerController.Teams.TeamRed:
                teamColor = Color.red;
                break;
            case PlayerController.Teams.TeamBlue:
                teamColor = Color.blue;
                break;
            default:
                break;
        }
        healthBar.fillRect.GetComponent<Image>().color = teamColor;

        if (pc.pv != null && pc.pv.IsMine == false)
        {
            UI.SetActive(false);
        }
    }

    public void UpdateHealth(int health)
    {
        healthBar.value = health;
    }
    public IEnumerator CooldownIndicator(Moves move, AttackEnum attackEnum)
    {
        _images[(int)attackEnum].color = Color.grey;

        for (float i = move._coolDown; i > 0;)
        {
            if (move._isReadyAnimating)
            {
                if (move._coolDown >= 1)
                {
                    yield return new WaitForSeconds(1);
                    i--;
                    _timerText[(int)attackEnum].text = i +"";
                }
                else
                {
                    i = 0;
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
        _timerText[(int)attackEnum].text = "";
        _images[(int)attackEnum].color = Color.white;
    }
}
