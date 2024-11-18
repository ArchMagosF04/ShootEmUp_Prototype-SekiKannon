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

    private IPreFireEffect preFireEffect;

    [SerializeField] private SoundLibraryObject soundLibrary;
    [SerializeField] private int libraryClipIndex = 0;

    private void Awake()
    {
        factory = GetComponentInParent<BulletFactory>();
        preFireEffect = GetComponent<IPreFireEffect>();
    }

    public override void Shoot()
    {
        if (!isShoting)
        {
            if (preFireEffect != null)
            {
                preFireEffect.ExecuteEffect();
            }

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

            if (soundLibrary != null)
            {
                SoundManager.Instance.CreateSound().WithSoundData(soundLibrary.soundData[libraryClipIndex]).WithRandomPitch().Play();
            }

            lap++;
        }

        yield return new WaitForSeconds(pattern.EndWait);

        isShoting = false;
    }

    public void SimpleShot(string name, Vector2 origin, Vector2 direction, float velocity)
    {
        if (soundLibrary != null)
        {
            SoundManager.Instance.CreateSound().WithSoundData(soundLibrary.soundData[libraryClipIndex]).WithRandomPitch().Play();
        }

        Bullet_Controller bullet = factory.CreateBullet(name);
        bullet.transform.position = origin;
        bullet.transform.up = direction;
        bullet.Bullet_Movement.AssignMovement(direction * velocity);
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
            SimpleShot(settings.BulletName, origin, bulletDirection, settings.BulletSpeed);
        }
    }
}
