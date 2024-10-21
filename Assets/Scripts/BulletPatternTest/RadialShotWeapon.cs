using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShotWeapon : MonoBehaviour
{
    [SerializeField] private RadialShotPattern shotPattern;

    private bool onShotPattern = false;

    private void Update()
    {
        if (onShotPattern)
            return;

        StartCoroutine(ExecuteRadialShotPattern(shotPattern));
    }

    private IEnumerator ExecuteRadialShotPattern(RadialShotPattern pattern)
    {
        onShotPattern = true;

        int lap = 0;
        Vector2 aimDirection = transform.up;
        Vector2 center = transform.position;

        while(lap < pattern.Repetitions)
        {
            for (int i = 0; i < pattern.Repetitions; i++)
            {
                //Where we left off
            }

            lap++;
        }

        onShotPattern = false;
    }
}
