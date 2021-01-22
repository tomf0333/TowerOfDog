using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string Monster_name = "";
    public double damage = 0;
    public double maxHealth = 0;
    public double currentHealth = 0;
    public bool alive = true;

    public Monster(string name, double damage, int maxHealth, int currentHealth)
    {
        this.Monster_name = name;
        this.damage = damage;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }

    public void TakeDamage(double damage)
    {
        currentHealth = Math.Round(currentHealth - damage, 2);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            this.alive = false;
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
