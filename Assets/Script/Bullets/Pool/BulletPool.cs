using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public ObjectPool<Bullet_Controller> pool;

    private IPoolsBullets creator;

    private Transform spawnPoint;

    [SerializeField] private int defaultCapacity = 30;
    [SerializeField] private int maxSize = 40;

    private void Awake()
    {
        creator = GetComponent<IPoolsBullets>();
    }

    private void Start()
    {
        pool = new ObjectPool<Bullet_Controller>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, defaultCapacity, maxSize);
    }

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
        bullet.transform.rotation = spawnPoint.rotation;

        bullet.gameObject.SetActive(true);
    }

    private Bullet_Controller CreatePooledItem()
    {
        Bullet_Controller creation = Instantiate(creator.GetBulletPrefab(), spawnPoint.position, spawnPoint.rotation, spawnPoint);

        creation.SetPool(pool);

        return creation;
    }

    public void ChangeSpawnpoint(Transform transform)
    {
        spawnPoint = transform;
    }
}
