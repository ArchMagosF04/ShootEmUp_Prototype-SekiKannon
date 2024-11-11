using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletHell System/Linear Shot Pattern")]
public class LinearShotPattern : ScriptableObject
{
    public int Repetitions;
    [Range(-180f, 180f)] public float AngleOffsetBetweenReps = 0f;
    public float StartWait = 0f;
    public float EndWait = 0f;
    public LinearShotSettings[] PatternSettings;
}
