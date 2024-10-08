/*      Handling Player Details on each level
        Health
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable Initialization

    [Header("Health")]
    [SerializeField] HealthBar healthBar;
    [SerializeField] float maxHealth = 100;

    float currentHealth;

    #endregion

    #region Getters and Setters
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    #endregion

    private void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void DecreaseHealth(float damage)
    {
        if (!healthBar) return;

        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);
    }

}
