using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Bullet_Impact), typeof(Bullet_Movement))]
public class Bullet_Controller : MonoBehaviour
{
    [SerializeField] private BulletSO bulletData;
    public BulletSO BulletData => bulletData;


    private float lifeTime;

    private ObjectPool<Bullet_Controller> pool;

    private Bullet_Movement bullet_Movement;
    public Bullet_Movement Bullet_Movement => bullet_Movement;

    private void Awake()
    {
        lifeTime = bulletData.InitialLifeTime;
        bullet_Movement = GetComponent<Bullet_Movement>();
    }

    private void OnEnable()
    {
        lifeTime = bulletData.InitialLifeTime;
    }

    private void Update()
    {
        BulletLifeTime();
    }

    private void BulletLifeTime()
    {
        if (lifeTime <= 0)
        {
            DestroySelf();
        }

        lifeTime -= Time.deltaTime;
    }

    public void SetPool(ObjectPool<Bullet_Controller> objectPool)
    {
        pool = objectPool;
    }

    public void DestroySelf()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ResetLifeTime()
    {
        lifeTime = bulletData.InitialLifeTime;
    }
}
