using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private GreenBulletSO bulletData;

    private Bullet_Controller controller;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
    }

    private void OnEnable()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = BulletColorManager.Instance.CircleColor;
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
        player_Shield.TakeDamage(bulletData.ShieldDamage);
        if (bulletData.BlockHeal > 0)
        {
            HealPlayer(bulletData.BlockHeal);
        }
        
        controller.DestroySelf();
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeSafeDamage(bulletData.ShieldDamage);
        HealPlayer(bulletData.ParryHeal);
        controller.DestroySelf();
    }
}
