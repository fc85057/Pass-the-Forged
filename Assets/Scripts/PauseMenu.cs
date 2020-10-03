using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private Texture2D cursor;

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

    private bool isPaused;
    private VikingStats vikingStats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void Resume()
    {
        UnPause();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Pause()
    {
        vikingStats = GameManager.Instance.CurrentViking.Stats;
        UpdateText();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void UpdateText()
    {
        maxHealth.text = "Health: " + vikingStats.maxHealth.ToString();
        maxStamina.text = "Stamina: " + vikingStats.maxStamina.ToString();
        meleeDamage.text = "Melee Damage: " + vikingStats.meleeDamage.ToString();
        rangeDamage.text = "Range Damage: " + vikingStats.rangeDamage.ToString();
        healingAmount.text = "Healing: " + vikingStats.healing.ToString();
        playerImmunities.text = GetImmunities();
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private string GetImmunities()
    {
        string immunities = "";
        foreach (Element immunity in vikingStats.immunities)
        {
            immunities += immunity.ToString() + " ";
        }
        if (string.IsNullOrEmpty(immunities))
        {
            return "Immmunities: None";
        }
        else
        {
            return ("Immunities: " + immunities);
        }

    }

}
