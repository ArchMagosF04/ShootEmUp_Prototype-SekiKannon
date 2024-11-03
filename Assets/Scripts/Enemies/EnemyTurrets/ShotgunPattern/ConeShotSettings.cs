using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConeShotSettings
{
    [Header("Bullet Type")]
    public string BulletName = "BasicBullet";

    [Header("Base Settings")]
    [Range(5f, 180f)] public float ConeArcAngle = 45f;
    [Range(2f, 100f)] public int NumberOfBullets = 5;
    public float BulletSpeed = 10f;
    public float CooldownAfterShot;

    [Header("Offsets")]
    [Range(-1f, 1f)] public float PhaseOffset = 0f;
    [Range(-180f, 180f)] public float AngleOffset = 0f;
}
