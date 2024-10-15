using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
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
        bulletPool.SetBulletPrefab(bulletPrefab);
    }

    private void Update()
    {
        if(shootCooldown <= 0f)
        {
            bulletPool.pool.Get();
            shootCooldown = shootingInterval;
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }
}
