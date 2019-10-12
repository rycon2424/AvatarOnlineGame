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
        _playerName.text = pc.pv.Owner.NickName;
        UpdateHealth(pc._health);
        if (pc.pv.IsMine == false)
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
