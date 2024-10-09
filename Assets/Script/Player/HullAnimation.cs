using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake() //Gets the animator
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable() //Subscribes to the event to listen when the player's health changes.
    {
        Player_Health.OnDamageReceived += UpdateSprite;
    }

    private void UpdateSprite(float maxValue, float currentValue) //Changes the animation depending on the percentage of health.
    {
        float health = currentValue / maxValue;

        switch (health)
        {
            case var expression when (health <=  0.25f):
                anim.SetInteger("HealthValue", 4);
                break;
            case var expression when (health <= 0.5f):
                anim.SetInteger("HealthValue", 3);
                break;
            case var expression when (health <= 0.75f):
                anim.SetInteger("HealthValue", 2);
                break;
            default:
                anim.SetInteger("HealthValue", 1);
                break;
        }
    }

    private void OnDisable() //Unsubscribes to avoid any problems.
    {
        Player_Health.OnDamageReceived -= UpdateSprite;
    }
}
