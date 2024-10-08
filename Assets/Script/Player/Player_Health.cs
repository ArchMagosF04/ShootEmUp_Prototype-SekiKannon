using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10; //Sets the maximum value of health points.
    private int currentHealth;

    [SerializeField] private HealthBar healthSlider;

    [SerializeField] private Animator shipHullAnimator;

    private void Start()
    {
        currentHealth = maxHealth; //Fills all health points.
        healthSlider.SetMaxHealth(maxHealth, currentHealth);
    }

    public void TakeDamage(int damageReceived) //Interface function. Receives the damage value to be subtracted from the health.
    {
        currentHealth -= damageReceived;
        UpdateShipSprite(currentHealth);

        healthSlider.SetHealth(currentHealth);

        if (currentHealth <= 0) //If the healthbar reaches 0 then die.
        {
            currentHealth = 0; //Avoids health from going into negative values.

            PlayerDeath();
        }
    }

    public void HealPlayer(int healAmount) //Heals the player by the value given.
    {
        currentHealth += healAmount;
        UpdateShipSprite(currentHealth);

        if (currentHealth >= maxHealth) //Prevents health from going above its maximum value.
        {
            currentHealth = maxHealth;
        }

        healthSlider.SetHealth(currentHealth);
    }

    private void UpdateShipSprite(int health)
    {
        switch (health)
        {
            case var expression when (health <= Mathf.FloorToInt(maxHealth*0.25f)):
                shipHullAnimator.SetInteger("HealthValue", 4);
                break;
            case var expression when (health <= Mathf.FloorToInt(maxHealth * 0.5f)):
                shipHullAnimator.SetInteger("HealthValue", 3);
                break;
            case var expression when (health <= Mathf.FloorToInt(maxHealth * 0.75f)):
                shipHullAnimator.SetInteger("HealthValue", 2);
                break;
            default:
                shipHullAnimator.SetInteger("HealthValue", 1);
                break;
        }
    }

    public void PlayerDeath() 
    {
        Destroy(this.gameObject);
    }
}
