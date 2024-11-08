using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeShotWeapon : AbstractTurret
{
    [SerializeField] private ConeShotPattern shotPattern;

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
    //        StartCoroutine(ExecuteConeShotPattern(shotPattern));
    //    }
    //}

    public override void Shoot()
    {
        if (!isShoting)
        {
            StartCoroutine(ExecuteConeShotPattern(shotPattern));
        }
    }

    private IEnumerator ExecuteConeShotPattern(ConeShotPattern pattern)
    {
        isShoting = true;

        int lap = 0;
        Vector2 aimDirection = transform.up;
        Vector2 startingOffset = aimDirection;

        yield return new WaitForSeconds(pattern.StartWait);

        while (lap < pattern.Repetitions)
        {
            if (lap > 0 && pattern.AngleOffsetBetweenReps != 0)
            {
                startingOffset = aimDirection.Rotate(pattern.AngleOffsetBetweenReps);
            }

            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                aimDirection = startingOffset;
                aimDirection = aimDirection.Rotate(-pattern.PatternSettings[i].ConeArcAngle/2);

                ConeShot(pattern.PatternSettings[i].ConeArcAngle, transform.position, aimDirection, pattern.PatternSettings[i]);
                yield return new WaitForSeconds(pattern.PatternSettings[i].CooldownAfterShot);
            }

            lap++;
        }

        yield return new WaitForSeconds(pattern.EndWait);

        isShoting = false;
    }

    public void SimpleShot(string name, Vector2 origin, Vector2 direction, float velocity)
    {
        Bullet_Controller bullet = factory.CreateBullet(name);
        bullet.transform.position = origin;
        bullet.transform.up = direction;
        bullet.Bullet_Movement.AssignMovement(direction * velocity);
    }

    public void ConeShot(float coneArc, Vector2 origin, Vector2 aimDirection, ConeShotSettings settings)
    {
        float angleBetweenBullets = coneArc / (settings.NumberOfBullets-1);

        if (settings.AngleOffset != 0f || settings.PhaseOffset != 0f)
        {
            aimDirection = aimDirection.Rotate(settings.AngleOffset + (settings.PhaseOffset * angleBetweenBullets));
        }

        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;

            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
            SimpleShot(settings.BulletName, origin, bulletDirection, settings.BulletSpeed);
        }
    }
}
