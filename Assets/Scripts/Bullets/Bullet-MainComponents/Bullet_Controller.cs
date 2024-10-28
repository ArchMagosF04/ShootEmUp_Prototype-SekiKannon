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

    private float lifeTime = 0f;

    private ObjectPool<Bullet_Controller> pool;

    private Bullet_Movement bullet_Movement;
    public Bullet_Movement Bullet_Movement => bullet_Movement;

    [SerializeField] private GameObject impactEffect;

    private void Awake()
    {
        bullet_Movement = GetComponent<Bullet_Movement>();
    }

    private void OnEnable()
    {
        lifeTime = 0f;
    }

    private void Update()
    {
        BulletLifeTime();
    }

    private void BulletLifeTime()
    {
        if (lifeTime > bulletData.InitialLifeTime)
        {
            DestroySelf();
        }

        lifeTime += Time.deltaTime;
    }

    public void SetPool(ObjectPool<Bullet_Controller> objectPool)
    {
        pool = objectPool;
    }

    public void DestroySelf()
    {
        if(impactEffect != null)
        {
            GameObject effect = Instantiate(impactEffect);
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
        }

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
        lifeTime = 0f;
    }
}
