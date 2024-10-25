using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 500; //Sets the maximum value of health points.
    private int currentHealth;

    public static event Action<float, float> OnDamageReceived;

    private void Start()
    {
        currentHealth = maxHealth; //Fills all health points.
        OnDamageReceived?.Invoke(maxHealth, currentHealth);
    }

    public void TakeDamage(int damageReceived)
    {
        currentHealth -= damageReceived;
        OnDamageReceived?.Invoke(maxHealth, currentHealth);

        if (currentHealth <= 0) //If the healthbar reaches 0 then die.
        {
            currentHealth = 0; //Avoids health from going into negative values.

            Destroy(gameObject);
        }
    }
}
