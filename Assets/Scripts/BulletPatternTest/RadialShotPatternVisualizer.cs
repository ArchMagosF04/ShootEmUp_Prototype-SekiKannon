using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class RadialShotPatternVisualizer : MonoBehaviour
{
    [SerializeField] private RadialShotPattern pattern;
    [SerializeField] private float radius;
    [SerializeField] private Color iconColor = Color.red;
    [SerializeField, Range(0f, 5f)] private float testTime;

    private void OnDrawGizmos()
    {
        if (pattern == null)
        {
            return;
        }

        Gizmos.color = iconColor;

        int lap = 0;
        Vector2 aimDirection = transform.up;
        Vector2 center = transform.position;

        float timer = testTime;

        while (timer > 0f && lap < pattern.Repetitions)
        {
            if (lap > 0 && pattern.AngleOffsetBetweenReps != 0)
            {
                aimDirection = aimDirection.Rotate(pattern.AngleOffsetBetweenReps);
            }

            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                if (timer < 0f)
                    break;

                DrawRadialShot(pattern.PatternSettings[i], timer, aimDirection);
                
                timer -= pattern.PatternSettings[i].CooldownAfterShot;
            }

            lap++;
        }
    }

    private void DrawRadialShot(RadialShotSettings settings, float lifeTime, Vector2 aimDirection)
    {
        float angleBetweenBullets = 360f / settings.NumberOfBullets;

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
            Vector2 bulletPosition = (Vector2)transform.position + (bulletDirection * settings.BulletSpeed * lifeTime);
            Gizmos.DrawSphere(bulletPosition, radius);
            Gizmos.DrawWireSphere(bulletPosition, radius);
        }
    }
}
