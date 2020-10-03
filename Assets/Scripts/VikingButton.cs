using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VikingButton : MonoBehaviour
{

    [SerializeField]
    Texture2D cursor;

    [SerializeField]
    private Text maxHealth;
    [SerializeField]
    private Text maxStamina;
    [SerializeField]
    private Text meleeDamage;
    [SerializeField]
    private Text rangeDamage;
    [SerializeField]
    private Text healingAmount;
    [SerializeField]
    private Text playerImmunities;

    private VikingStats vikingStats;

    public void SetStats(VikingStats newStats)
    {
        vikingStats = newStats;

        maxHealth.text = "Health\n" + vikingStats.maxHealth.ToString();
        maxStamina.text = "Stamina\n" + vikingStats.maxStamina.ToString();
        meleeDamage.text = "Melee Damage\n" + vikingStats.meleeDamage.ToString();
        rangeDamage.text = "Range Damage\n" + vikingStats.rangeDamage.ToString();
        healingAmount.text = "Healing\n" + vikingStats.healing.ToString();
        playerImmunities.text = GetImmunities();
    }

    private string GetImmunities()
    {
        string immunities = "";
        foreach (Element immunity in vikingStats.immunities)
        {
            immunities += immunity.ToString() + "\n";
        }
        if (string.IsNullOrEmpty(immunities))
        {
            return "Immmunities\nNone";
        }
        else
        {
            return ("Immunities\n" + immunities);
        }
        
    }

    public void SelectViking(int i)
    {
        GameManager.Instance.SwitchVikings(i);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
