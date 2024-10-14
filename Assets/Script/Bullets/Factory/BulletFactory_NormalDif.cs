using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory_NormalDif : MonoBehaviour
{
    [SerializeField] private Bullet_Controller[] bullets;
    private Dictionary<string, Bullet_Controller> bulletDictionary;

    private void Start()
    {
        bulletDictionary = new Dictionary<string, Bullet_Controller>();

        foreach (var bullet in bullets)
        {
            bulletDictionary.Add(bullet.Id, bullet);
        }
    }

    public Bullet_Controller CreateBullet(string id)
    {
        if (bulletDictionary.ContainsKey(id))
        {
            return Instantiate(bulletDictionary[id]);
        }

        return null;
    }
}
