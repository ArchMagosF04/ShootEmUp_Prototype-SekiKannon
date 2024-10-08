using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private int damageToShields = 1;

    [SerializeField] private float speedOnDeflect = 50f;
    [SerializeField] private int damageOnDeflect = 4;

    private float initialSpeed;
    private int initialDamage;

    [SerializeField] private LayerMask whatDestroysBulletWhenDeflected;

    private Bullet_Controller controller;
    private Bullet_Movement bullet_Movement;
    private Bullet_Impact bullet_Impact;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
        bullet_Movement = GetComponent<Bullet_Movement>();
        bullet_Impact = GetComponent<Bullet_Impact>();

        initialDamage = bullet_Impact.Damage;
        initialSpeed = bullet_Movement.Speed;
    }

    private void OnEnable()
    {
        bullet_Movement.ModifySpeed(initialSpeed);
        bullet_Movement.Movement(transform.up);
        bullet_Impact.ModifyDamageAmount(initialDamage);
    }

    public void OnBlockEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeDamage(damageToShields);
        controller.DestroySelf();
        Debug.Log("Block");
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeSafeDamage(damageToShields);
        DeflectStats();
        ChangeTarget();
        Debug.Log("Parry");
    }

    private void ChangeTarget()
    {
        bullet_Impact.SwapCollisionLayer(whatDestroysBulletWhenDeflected);
        transform.up = (transform.parent.transform.position - transform.position).normalized;

        if (TryGetComponent<HomingBullet>(out HomingBullet homingBullet))
        {
            homingBullet.ChangeTarget(transform.parent.transform);
        } 
        else
        {
            bullet_Movement.Movement(transform.up);
        }

        controller.ResetLifeTime();
    }

    private void DeflectStats()
    {
        bullet_Movement.ModifySpeed(speedOnDeflect);
        bullet_Impact.ModifyDamageAmount(damageOnDeflect);
    }
}
