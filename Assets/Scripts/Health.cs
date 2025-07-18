using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        slider.value = health;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void AddHealth(int amount)
    {
        health += amount;

        // Batas maksimum health
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Update UI
        slider.value = health;
    }
}
