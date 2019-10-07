using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject UI;

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
        if (pc.pv.IsMine == false)
        {
            UI.SetActive(false);
        }
        UpdateHealth(pc._health);
    }

    public void UpdateHealth(int health)
    {
        healthBar.value = health;
    }
    public IEnumerator CooldownIndicator(Moves move, AttackEnum attackEnum)
    {
        _images[(int)attackEnum].color = Color.grey;

        Debug.Log(move._coolDown);

        for (float i = move._coolDown; i > 0;)
        {
            if (move._isReadyAnimating)
            {
                yield return new WaitForSeconds(1);
                i--;
                Debug.Log("text");
                _timerText[(int)attackEnum].text = i +"";
            }
            else
            {
                Debug.Log("wait");
                yield return new WaitForEndOfFrame();
            }
        }
        Debug.Log("reset");
        _timerText[(int)attackEnum].text = "";
        _images[(int)attackEnum].color = Color.white;
    }
}
