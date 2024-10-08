using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private int damageToShields = 1;

    [SerializeField] private int blockHealAmount = 1;
    [SerializeField] private int parryHealAmount = 2;

    private Bullet_Controller controller;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
    }

    private void HealPlayer(int healing)
    {
        Player_Health player_Health = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player_Health>();

        if (player_Health != null)
        {
            player_Health.HealPlayer(healing);
        }
    }

    public void OnBlockEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeDamage(damageToShields);
        HealPlayer(blockHealAmount);
        controller.DestroySelf();
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeSafeDamage(damageToShields);
        HealPlayer(parryHealAmount);
        controller.DestroySelf();
    }
}
