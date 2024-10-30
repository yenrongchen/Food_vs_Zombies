using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    private float maxHP;
    private float curHP;

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        updateHealthBar();
    }

    public void getDamaged(float dmg)
    {
        curHP -= dmg;
        curHP = Mathf.Clamp(curHP, 0, maxHP);
        updateHealthBar();
    }

    void updateHealthBar()
    {
        float hpRatio = curHP / maxHP;
        healthBar.fillAmount = hpRatio;
    }

    public void setMaxHP(float maxHP)
    {
        this.maxHP = maxHP;
    }
}
