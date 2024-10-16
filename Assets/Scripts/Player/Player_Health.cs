using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 20; //Sets the maximum value of health points.
    private int currentHealth;

    public static event Action<float, float> OnDamageReceived; //Event that will be called every time the player takes damage.
    public static event Action<float, float> OnHealthHealed; //Event that will be called every time the player heals.

    private void Start()
    {
        currentHealth = maxHealth; //Fills all health points.
        OnDamageReceived?.Invoke(maxHealth, currentHealth);
    }

    public void TakeDamage(int damageReceived) //Interface function. Receives the damage value to be subtracted from the health.
    {
        currentHealth -= damageReceived;

        OnDamageReceived?.Invoke(maxHealth, currentHealth);

        if (currentHealth <= 0) //If the healthbar reaches 0 then die.
        {
            currentHealth = 0; //Avoids health from going into negative values.

            PlayerDeath();
        }
    }

    public void HealPlayer(int healAmount) //Heals the player by the value given.
    {
        currentHealth += healAmount;

        if (currentHealth >= maxHealth) //Prevents health from going above its maximum value.
        {
            currentHealth = maxHealth;
        }

        OnHealthHealed?.Invoke(maxHealth, currentHealth);
    }

    public void PlayerDeath() 
    {
        Destroy(this.gameObject);
    }
}
