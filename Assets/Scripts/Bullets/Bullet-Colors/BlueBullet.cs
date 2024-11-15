using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private DefaultColorBulletSO bulletData;

    private Bullet_Controller controller;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
    }

    private void OnEnable()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = BulletColorManager.Instance.SquareColor;
    }

    public void OnBlockEffect(Player_Shield player_Shield) //When blocked, the bullet is destroyed and damages the shield.
    {
        player_Shield.TakeDamage(bulletData.ShieldDamage);
        controller.DestroySelf();
    }

    public void OnParryEffect(Player_Shield player_Shield) //When parried, the bullet is destroyed and damages the shield, but won't fill the bar to the max.
    {
        player_Shield.TakeSafeDamage(bulletData.ShieldDamage);
        controller.DestroySelf();
    }
}
