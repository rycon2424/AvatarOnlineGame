using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;
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

}
