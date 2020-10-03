using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider staminaSlider;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text staminaText;

    [SerializeField]
    private Text meleeDamage;
    [SerializeField]
    private Text rangeDamage;
    [SerializeField]
    private Text healingAmount;
    [SerializeField]
    private Text playerImmunities;

    private void Awake()
    {
        Viking.OnHealthChanged += ChangeHealth;
        Viking.OnStaminaChanged += ChangeStamina;
    }

    private void ChangeHealth(Viking viking, int currentHealth, int maxHealth)
    {
        if (viking == GameManager.Instance.CurrentViking)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        
        //healthText.text = $"{currentHealth}/{maxHealth}";
    }

    private void ChangeStamina(Viking viking, int currentStamina, int maxStamina)
    {
        if (viking == GameManager.Instance.CurrentViking)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
        
        //staminaText.text = $"{currentStamina}/{maxStamina}";
    }

    private void SetMelee(int melee)
    {
        meleeDamage.text = $"Melee: {melee}";
    }

    private void SetRange(int range)
    {
        rangeDamage.text = $"Range: {range}";
    }

    private void SetHealingAmount(int healing)
    {
        healingAmount.text = $"Healing: {healing}";
    }

    private void SetImmunities(string immunities)
    {
        playerImmunities.text = $"Immunities: {immunities}";
    }


}
