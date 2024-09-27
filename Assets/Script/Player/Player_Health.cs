using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 10f;
    private float currentHealth;

    [SerializeField] private HealthBar healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.SetMaxHealth(maxHealth, currentHealth);
    }

    public void TakeDamage(float damageReceived)
    {
        currentHealth -= damageReceived;

        healthSlider.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Destroy(this.gameObject);
        }
    }

    public void HealPlayer(float healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthSlider.SetHealth(currentHealth);
    }
}
