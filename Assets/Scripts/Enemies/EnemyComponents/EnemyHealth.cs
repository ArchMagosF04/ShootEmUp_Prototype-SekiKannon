using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 500; //Sets the maximum value of health points.
    public int CurrentHealth { get; private set; }

    public event Action<float, float> OnDamageReceived;
    public event Action OnEnemyDeath;

    private DamageFlash damageFlash;

    private void Start()
    {
        damageFlash = GetComponent<DamageFlash>();
        CurrentHealth = maxHealth; //Fills all health points.

        HUDManager.Instance.CreateBossHealthBar(this);

        OnDamageReceived?.Invoke(maxHealth, CurrentHealth);
    }

    public void TakeDamage(int damageReceived)
    {
        CurrentHealth -= damageReceived;

        if (CurrentHealth <= 0) //If the healthbar reaches 0 then die.
        {
            CurrentHealth = 0; //Avoids health from going into negative values.
            OnDamageReceived?.Invoke(maxHealth, CurrentHealth);

            OnEnemyDeath?.Invoke();

            return;
        }

        damageFlash.CallDamageFlash(0);
        OnDamageReceived?.Invoke(maxHealth, CurrentHealth);
    }
}
