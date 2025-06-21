using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private bool isDead = false;

    public HealthBarBehavior healthBar; // Tambahkan ini

    void Start()
    {
        currentHealth = maxHealth;

        // Optional, kalau kamu belum drag di Inspector:
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBarBehavior>();
        }

        // Set awal health bar
        if (healthBar != null)
        {
            healthBar.SetHeatlh(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.SetHeatlh(currentHealth, maxHealth);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
}
