using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet_Controller[] bullets;
    private Dictionary<string, Bullet_Controller> bulletDictionary;

    private Dictionary<string, BulletPool> poolDictionary;

    private void Start()
    {
        bulletDictionary = new Dictionary<string, Bullet_Controller>();

        poolDictionary = new Dictionary<string, BulletPool>();

        foreach (var bullet in bullets)
        {
            bulletDictionary.Add(bullet.name, bullet);
        }
    }

    public Bullet_Controller CreateBullet(string id)
    {
        if (bulletDictionary.ContainsKey(id))
        {
            if(!poolDictionary.ContainsKey(id))
            {
                BulletPool newBulletPool = gameObject.AddComponent<BulletPool>();

                newBulletPool.SetBulletPrefab(bulletDictionary[id]);

                poolDictionary.Add(id, newBulletPool);
            }

            Bullet_Controller newBullet = poolDictionary[id].pool.Get();

            return newBullet;
        }

        return null;
    }
}
