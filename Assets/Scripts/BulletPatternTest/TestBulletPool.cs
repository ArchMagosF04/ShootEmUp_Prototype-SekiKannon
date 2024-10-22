using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTestBulletPool : MonoBehaviour
{
    //private static TestBulletPool instance;

    //public static TestBulletPool Instance
    //{
    //    get 
    //    {
    //        if (instance == null)
    //            Debug.LogError("BulletPoolInstanceMissing.");

    //        return instance;
    //    }
    //}

    [SerializeField] private Bullet_Controller bulletPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private List<Bullet_Controller> bulletPool = new List<Bullet_Controller>();

    private void Awake()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //else
        //{
        //    instance = this;
        //}

        AddBulletsToPool(initialPoolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet_Controller bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public Bullet_Controller RequestBullet()
    {
        for (int i = 0; i < bulletPool.Count ; i++)
        {
            if (!bulletPool[i].gameObject.activeSelf)
            {
                bulletPool[i].gameObject.SetActive(true);
                return bulletPool[i];
            }
        }
        AddBulletsToPool(1);
        bulletPool[bulletPool.Count - 1].gameObject.SetActive(true);
        return bulletPool[bulletPool.Count - 1];
    }
}
