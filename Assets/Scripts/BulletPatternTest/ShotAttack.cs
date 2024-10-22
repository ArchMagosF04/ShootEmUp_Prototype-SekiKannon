using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAttack : MonoBehaviour
{
    //public static void SimpleShot(Vector2 origin, Vector2 velocity)
    //{
    //    Bullet_Controller bullet = TestBulletPool.Instance.RequestBullet();
    //    bullet.transform.position = origin;
    //    bullet.Bullet_Movement.AssignMovement(velocity);
    //}

    //public static void RadialShot(Vector2 origin, Vector2 aimDirection, RadialShotSettings settings)
    //{
    //    float angleBetweenBullets = 360 / settings.NumberOfBullets;

    //    if (settings.AngleOffset != 0f || settings.PhaseOffset != 0f)
    //    {
    //        aimDirection = aimDirection.Rotate(settings.AngleOffset + (settings.PhaseOffset * angleBetweenBullets));
    //    }
            
    //    for (int i = 0; i < settings.NumberOfBullets; i++)
    //    {
    //        float bulletDirectionAngle = angleBetweenBullets * i;

    //        if (settings.RadialMask && bulletDirectionAngle > settings.MaskAngle)
    //        {
    //            break;
    //        }

    //        Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
    //        SimpleShot(origin, bulletDirection * settings.BulletSpeed);
    //    }
    //}
}
