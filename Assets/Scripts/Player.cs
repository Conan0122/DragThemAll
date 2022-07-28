// Handling Player Details
// Health
// Coins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable Initialization

    [SerializeField] HealthBar healthBar;

    [SerializeField] float maxHealth = 100;
    float currentHealth;

    #endregion

    #region Getters and Setters
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
        }
    }
    #endregion

    private void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void DecreaseHealth(float damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);
    }

}
