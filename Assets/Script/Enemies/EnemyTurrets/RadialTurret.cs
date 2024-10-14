using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPool))]
public class RadialTurret : MonoBehaviour
{
    [SerializeField] private int numberOfBullets;
    [SerializeField] private Bullet_Controller bulletPrefab;

    private Vector3 startingPoint;
    private const float radius = 1f;

    private BulletPool bulletPool;

    private Rigidbody2D rb;

    private void Awake()
    {
        bulletPool = GetComponent<BulletPool>();
        bulletPool.ChangeSpawnpoint(transform);
        bulletPool.SetBulletPrefab(bulletPrefab);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            startingPoint = transform.position;
            SpawnProjectile(numberOfBullets);
        }
    }

    private void SpawnProjectile(int bulletCount)
    {
        float angleStep = 360 / bulletCount;

        Quaternion initialRotation = transform.rotation;

        for (int i = 0; i < numberOfBullets; i++)
        {
            bulletPool.ChangeSpawnpoint(transform);

            bulletPool.pool.Get();

            transform.Rotate(0,0,angleStep);
        }

        transform.rotation = initialRotation;
    }

    public Bullet_Controller GetBulletPrefab()
    {
        return bulletPrefab;
    }
}
