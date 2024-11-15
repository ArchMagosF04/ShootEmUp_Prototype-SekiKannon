using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBullet : MonoBehaviour, IParryEffect
{
    [SerializeField] private OrangeBulletSO bulletData;

    private Transform creator;

    private Bullet_Controller controller;
    private Bullet_Movement bullet_Movement;
    private Bullet_Impact bullet_Impact;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
        bullet_Movement = GetComponent<Bullet_Movement>();
        bullet_Impact = GetComponent<Bullet_Impact>();

        creator = GameManager.Instance.BossCharacter.transform;
    }

    private void OnEnable()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = BulletColorManager.Instance.RonboidColor;
    }

    public void OnBlockEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeDamage(bulletData.ShieldDamage);
        controller.DestroySelf();
    }

    public void OnParryEffect(Player_Shield player_Shield)
    {
        player_Shield.TakeSafeDamage(bulletData.ShieldDamage);
        DeflectStats();
        ChangeTarget();
    }

    private void ChangeTarget()
    {
        bullet_Impact.SwapCollisionLayer(bulletData.whatDestroysBulletWhenDeflected);
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
        bullet_Movement.MultiplySpeed(bulletData.SpeedOnDeflect);
        bullet_Impact.ModifyDamageMultiplier(bulletData.DamageOnDeflect);
    }
}
