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

    private Transform creator;

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
        initialSpeed = bullet_Movement.NormalSpeed;
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
        transform.up = (creator.position - transform.position).normalized;

        if (TryGetComponent<HomingBullet>(out HomingBullet homingBullet))
        {
            homingBullet.ChangeTarget(creator);
        } 
        else
        {
            bullet_Movement.Movement(transform.up);
        }

        controller.ResetLifeTime();
    }

    public void SetCreator(Transform transform)
    {
        creator = transform;
    }

    private void DeflectStats()
    {
        bullet_Movement.ModifySpeed(speedOnDeflect);
        bullet_Impact.ModifyDamageAmount(damageOnDeflect);
    }
}
