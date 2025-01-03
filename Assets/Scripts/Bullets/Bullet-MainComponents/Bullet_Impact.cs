using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Impact : MonoBehaviour
{
    private Bullet_Controller controller;

    private float damageMultiplier = 1f;

    private LayerMask whatDestroysBullet;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
    }

    private void OnEnable()
    {
        whatDestroysBullet = controller.BulletData.WhatDestroysBullet;
        damageMultiplier = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTargetHit(collision);
    }

    private void OnTargetHit(Collider2D collision)
    {
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {

            //Damage the target
            DealDamage(collision);

            //Eliminate the bullet
            controller.DestroySelf();
        }
    }

    private void DealDamage(Collider2D target)
    {
        IDamageable iDamageable = target.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.TakeDamage(Mathf.RoundToInt(controller.BulletData.Damage*damageMultiplier));
            controller.ExecuteDeathEffect = false;
        }
    }

    public void ModifyDamageMultiplier(float amount)
    {
        damageMultiplier = amount;
    }

    public void SwapCollisionLayer(LayerMask mask)
    {
        whatDestroysBullet = mask;
    }
}
