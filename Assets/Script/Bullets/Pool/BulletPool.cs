using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public ObjectPool<Bullet_Controller> pool;

    private Bullet_Controller bulletPrefab;

    private Transform spawnPoint;

    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private void Awake()
    {
        pool = new ObjectPool<Bullet_Controller>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, defaultCapacity, maxSize);
    }

    //private void Start()
    //{
    //    for (int i = 0; i < defaultCapacity; i++)
    //    {
    //        CreatePooledItem();
    //    }
    //}


    private void OnDestroyPoolObject(Bullet_Controller bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void OnReturnedToPool(Bullet_Controller bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Bullet_Controller bullet)
    {
        bullet.transform.position = spawnPoint.position;
        bullet.transform.localRotation = spawnPoint.rotation;

        bullet.gameObject.SetActive(true);

        bullet.Bullet_Movement.Movement(spawnPoint.up);
    }

    private Bullet_Controller CreatePooledItem()
    {
        Bullet_Controller creation = Instantiate(bulletPrefab);

        creation.transform.position = spawnPoint.position;
        creation.transform.localRotation = spawnPoint.rotation;

        creation.Bullet_Movement.Movement(spawnPoint.up);

        creation.SetPool(pool);

        //creation.gameObject.SetActive(false);

        return creation;
    }

    public void ChangeSpawnpoint(Transform transform)
    {
        spawnPoint = transform;
    }

    public void SetBulletPrefab(Bullet_Controller bullet)
    {
        bulletPrefab = bullet;
    }
}
