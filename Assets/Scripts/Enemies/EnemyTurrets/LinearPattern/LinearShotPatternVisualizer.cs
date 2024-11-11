using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LinearShotPatternVisualizer : MonoBehaviour
{
    [SerializeField] private LinearShotPattern pattern;
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

                DrawSimpleShot(pattern.PatternSettings[i], timer, aimDirection);

                timer -= pattern.PatternSettings[i].CooldownAfterShot;
            }

            lap++;
        }
    }

    private void DrawSimpleShot(LinearShotSettings settings, float lifeTime, Vector2 aimDirection)
    {
        Vector2 bulletPosition = (Vector2)transform.position + (aimDirection * settings.BulletSpeed * lifeTime);
        Gizmos.DrawSphere(bulletPosition, radius);
    }
}
