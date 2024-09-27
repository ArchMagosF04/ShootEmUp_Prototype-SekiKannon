using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Bullet_Impact), typeof(Bullet_Movement))]
public class Bullet_Controller : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    private float initialLifeTime;

    private ObjectPool<Bullet_Controller> pool;

    private void Awake()
    {
        initialLifeTime = lifeTime;
    }

    private void OnEnable()
    {
        lifeTime = initialLifeTime;
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
        //Destroy(this.gameObject);

        pool.Release(this);
    }

    public void ResetLifeTime()
    {
        lifeTime = initialLifeTime;
    }
}
