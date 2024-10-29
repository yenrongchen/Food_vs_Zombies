using UnityEngine;
using UnityEngine.UI;

public class FoodSoldierHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image curHpBar; // green part

    public void SetHealthBar(float hpPercent)
    {
        if (hpPercent <= 0)
        {
            hpPercent = 0;
            curHpBar.fillAmount = hpPercent;
            Destroy(gameObject, 0.1f); // to see full red before destroyed
        }
        curHpBar.fillAmount = hpPercent;
    }
}
