using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private int damageToShields = 1;

    private int initialDamage;

    private Bullet_Controller controller;
    private Bullet_Impact bullet_Impact;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
        bullet_Impact = GetComponent<Bullet_Impact>();

        initialDamage = bullet_Impact.Damage;
    }

    private void OnEnable()
    {
        bullet_Impact.ModifyDamageAmount(initialDamage);
    }

    public void OnBlockEffect(Player_Shield player_Shield)
    {
        player_Shield.shieldBroken = true;
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeDamage(damageToShields);
        bullet_Impact.ModifyDamageAmount(Mathf.FloorToInt(bullet_Impact.Damage*0.5f));
    }
}
