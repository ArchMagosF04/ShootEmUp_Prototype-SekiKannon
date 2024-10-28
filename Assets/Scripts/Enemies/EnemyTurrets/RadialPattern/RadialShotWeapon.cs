using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShotWeapon : AbstractTurret
{
    [SerializeField] private RadialShotPattern shotPattern;

    private bool isShoting = false;
    public override bool IsShoting { get => isShoting; set => isShoting = value; }

    private BulletFactory factory;

    private void Awake()
    {
        factory = GetComponentInParent<BulletFactory>();
    }

    //private void Update()
    //{
    //    if (!isShoting)
    //    {
    //        StartCoroutine(ExecuteRadialShotPattern(shotPattern));
    //    }
    //}

    public override void Shoot()
    {
        if (!isShoting)
        {
            StartCoroutine(ExecuteRadialShotPattern(shotPattern));
        }
    }

    private IEnumerator ExecuteRadialShotPattern(RadialShotPattern pattern)
    {
        isShoting = true;

        int lap = 0;
        Vector2 aimDirection = transform.up;

        yield return new WaitForSeconds(pattern.StartWait);

        while (lap < pattern.Repetitions)
        {
            if (lap > 0 && pattern.AngleOffsetBetweenReps != 0) 
            {
                aimDirection = aimDirection.Rotate(pattern.AngleOffsetBetweenReps);
            }

            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                RadialShot(new Vector2(transform.position.x, transform.position.y), aimDirection, pattern.PatternSettings[i]);
                yield return new WaitForSeconds(pattern.PatternSettings[i].CooldownAfterShot);
            }

            lap++;
        }

        yield return new WaitForSeconds(pattern.EndWait);

        isShoting = false;
    }

    public void SimpleShot(string name, Vector2 origin, Vector2 velocity)
    {
        Bullet_Controller bullet = factory.CreateBullet(name);
        bullet.transform.position = origin;
        bullet.Bullet_Movement.AssignMovement(velocity);
    }

    public void RadialShot(Vector2 origin, Vector2 aimDirection, RadialShotSettings settings)
    {
        float angleBetweenBullets = 360 / settings.NumberOfBullets;

        if (settings.AngleOffset != 0f || settings.PhaseOffset != 0f)
        {
            aimDirection = aimDirection.Rotate(settings.AngleOffset + (settings.PhaseOffset * angleBetweenBullets));
        }

        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;

            if (settings.RadialMask && bulletDirectionAngle > settings.MaskAngle)
            {
                break;
            }

            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
            SimpleShot(settings.BulletName, origin, bulletDirection * settings.BulletSpeed);
        }
    }
}