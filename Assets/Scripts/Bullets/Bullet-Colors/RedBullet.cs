using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private DefaultColorBulletSO bulletData;

    private float damageOnDeflect = 0.5f;

    private Bullet_Impact bullet_Impact;

    private void Awake()
    {
        bullet_Impact = GetComponent<Bullet_Impact>();
    }

    public void OnBlockEffect(Player_Shield player_Shield)
    {
        player_Shield.shieldBroken = true;
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeDamage(bulletData.ShieldDamage);
        bullet_Impact.ModifyDamageMultiplier(damageOnDeflect);
    }
}
