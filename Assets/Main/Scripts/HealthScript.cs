using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
#region  Variables
    public int maxHealth;
    public int _currentHealth => currentHealth;   
    private int currentHealth;

#endregion

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth() 
    {
        maxHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Heal(int heal)
    {
        currentHealth += heal;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
