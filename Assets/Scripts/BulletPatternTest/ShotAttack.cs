using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour
{
    public static void SimpleShot(Vector2 origin, Vector2 velocity)
    {
        Bullet bullet = TestBulletPool.Instance.RequestBullet();
        bullet.transform.position = origin;
        bullet.Velocity = velocity;
    }

    public static void RadialShot(Vector2 origin, Vector2 aimDirection, RadialShotSettings settings)
    {
        float andgleBetweenBullets = 360 / settings.NumberOfBullets;

        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = andgleBetweenBullets * i;

            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
            SimpleShot(origin, bulletDirection * settings.BulletSpeed);
        }
    }
}
