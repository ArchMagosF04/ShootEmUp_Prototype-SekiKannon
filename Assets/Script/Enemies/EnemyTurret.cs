using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour, IPoolsBullets
{
    [SerializeField] private Bullet_Controller bulletPrefab;
    [SerializeField] private Transform cannon;

    [SerializeField] private float shootingInterval = 1f;
    private float shootCooldown;

    private BulletPool bulletPool;

    private void Awake()
    {
        bulletPool = GetComponent<BulletPool>();
        bulletPool.ChangeSpawnpoint(cannon.transform);
    }

    private void Update()
    {
        if(shootCooldown <= 0f)
        {
            //Bullet_Controller bullet = Instantiate(bulletPrefab, cannon.position, cannon.rotation, cannon);
            bulletPool.pool.Get();
            shootCooldown = shootingInterval;
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    private Vector2 GetShootDirection()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        return (playerTransform.position - transform.position).normalized;
    }

    public Bullet_Controller GetBulletPrefab()
    {
        return bulletPrefab;
    }
}
