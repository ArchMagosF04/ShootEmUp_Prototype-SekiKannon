using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 500; //Sets the maximum value of health points.
    public int CurrentHealth {  get; private set; }

    public static event Action<float, float> OnDamageReceived;
    public static event Action OnEnemyDeath;

    private void Start()
    {
        CurrentHealth = maxHealth; //Fills all health points.
        OnDamageReceived?.Invoke(maxHealth, CurrentHealth);
    }

    public void TakeDamage(int damageReceived)
    {
        CurrentHealth -= damageReceived;
        OnDamageReceived?.Invoke(maxHealth, CurrentHealth);

        if (CurrentHealth <= 0) //If the healthbar reaches 0 then die.
        {
            CurrentHealth = 0; //Avoids health from going into negative values.

            OnEnemyDeath?.Invoke();
        }
    }
}
