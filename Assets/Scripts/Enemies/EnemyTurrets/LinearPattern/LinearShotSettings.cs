using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinearShotSettings
{
    [Header("Bullet Type")]
    public string BulletName = "Blue_Normal";

    [Header("Base Settings")]
    public float BulletSpeed = 10f;
    public float CooldownAfterShot;
    public bool AimAtPlayer = false;
}
