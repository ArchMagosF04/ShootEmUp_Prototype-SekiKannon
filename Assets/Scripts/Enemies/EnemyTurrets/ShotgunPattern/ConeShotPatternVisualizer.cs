using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeShotPatternVisualizer : MonoBehaviour
{
    [SerializeField] private ConeShotPattern pattern;
    [SerializeField] private float radius;
    [SerializeField] private Color iconColor = Color.red;
    [SerializeField, Range(0f, 10f)] private float testTime;

    private void OnDrawGizmos()
    {
        if (pattern == null)
        {
            return;
        }

        Gizmos.color = iconColor;

        int lap = 0;
        Vector2 aimDirection = transform.up;
        Vector2 startingOffset = aimDirection;

        float timer = testTime;

        while (timer > 0f && lap < pattern.Repetitions)
        {
            if (lap > 0 && pattern.AngleOffsetBetweenReps != 0)
            {
                startingOffset = aimDirection.Rotate(pattern.AngleOffsetBetweenReps);
            }

            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                if (timer < 0f)
                    break;

                aimDirection = startingOffset;
                aimDirection = aimDirection.Rotate(-pattern.PatternSettings[i].ConeArcAngle/2);

                DrawConeShot(pattern.PatternSettings[i], timer, aimDirection, pattern.PatternSettings[i].ConeArcAngle);

                timer -= pattern.PatternSettings[i].CooldownAfterShot;
            }

            lap++;
        }
    }

    private void DrawConeShot(ConeShotSettings settings, float lifeTime, Vector2 aimDirection, float coneArc)
    {
        float angleBetweenBullets = 0f;

        if (settings.NumberOfBullets > 1) angleBetweenBullets = coneArc / (settings.NumberOfBullets - 1);

        if (settings.AngleOffset != 0f || settings.PhaseOffset != 0f)
        {
            aimDirection = aimDirection.Rotate(settings.AngleOffset + (settings.PhaseOffset * angleBetweenBullets));
        }

        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;

            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
            Vector2 bulletPosition = (Vector2)transform.position + (bulletDirection * settings.BulletSpeed * lifeTime);
            Gizmos.DrawSphere(bulletPosition, radius);
        }
    }
}
