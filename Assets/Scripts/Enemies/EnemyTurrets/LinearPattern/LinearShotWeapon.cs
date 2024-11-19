using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LinearShotWeapon : AbstractTurret
{
    [SerializeField] private LinearShotPattern shotPattern;

    private bool isShoting = false;
    public override bool IsShoting { get => isShoting; set => isShoting = value; }

    private BulletFactory factory;

    private Transform target;

    private IPreFireEffect preFireEffect;

    [Header ("Sound Settings")]

    [SerializeField] private SoundLibraryObject soundLibrary;

    [SerializeField] private int libraryClipIndex = 0;

    private void Awake()
    {
        factory = GetComponentInParent<BulletFactory>();
        preFireEffect = GetComponent<IPreFireEffect>();
    }

    private void Start()
    {
        target = GameManager.Instance.PlayerCharacter.transform;
    }

    public override void Shoot()
    {
        if (!isShoting)
        {
            if (preFireEffect != null)
            {
                preFireEffect.ExecuteEffect();
            }

            StartCoroutine(ExecuteLinearShotPattern(shotPattern));
        }
    }

    private IEnumerator ExecuteLinearShotPattern(LinearShotPattern pattern)
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
                if (pattern.PatternSettings[i].AimAtPlayer)
                {
                    SimpleShot(pattern.PatternSettings[i].BulletName, new Vector2(transform.position.x, transform.position.y), AimAtPlayer(), pattern.PatternSettings[i].BulletSpeed);
                }
                else
                {
                    SimpleShot(pattern.PatternSettings[i].BulletName, new Vector2(transform.position.x, transform.position.y), aimDirection, pattern.PatternSettings[i].BulletSpeed);
                }
                
                yield return new WaitForSeconds(pattern.PatternSettings[i].CooldownAfterShot);
            }

            lap++;
        }

        yield return new WaitForSeconds(pattern.EndWait);

        isShoting = false;
    }

    private Vector2 AimAtPlayer()
    {
        Vector2 playerdirection = (target.position - transform.position).normalized;

        return playerdirection;
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
}
