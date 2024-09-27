using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Impact : MonoBehaviour
{
    private Bullet_Controller controller;

    [SerializeField] private float damage = 2f;
    public float Damage => damage;

    [SerializeField] private LayerMask whatDestroysBullet;

    private void Awake()
    {
        controller = GetComponent<Bullet_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTargetHit(collision);
    }

    private void OnTargetHit(Collider2D collision)
    {
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Special Effects

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
            iDamageable.TakeDamage(damage);
        }
    }

    public void ModifyDamageAmount(float amount)
    {
        damage = amount;
    }

    public void SwapCollisionLayer(LayerMask mask)
    {
        whatDestroysBullet = mask;
    }
}
