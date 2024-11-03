using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletHell System/Cone Shot Pattern")]
public class ConeShotPattern : ScriptableObject
{
    public int Repetitions;
    [Range(-180f, 180f)] public float AngleOffsetBetweenReps = 0f;
    public float StartWait = 0f;
    public float EndWait = 0f;
    public ConeShotSettings[] PatternSettings;
}
