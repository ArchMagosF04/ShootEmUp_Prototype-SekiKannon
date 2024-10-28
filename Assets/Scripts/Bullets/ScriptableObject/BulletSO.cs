using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewBulletData", menuName = "Scriptable Objects/Bullets/BulletData", order = 0)]
public class BulletSO : ScriptableObject
{
    [field: SerializeField] public float InitialLifeTime { get; private set; } = 5f;
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public LayerMask WhatDestroysBullet { get; private set; }

}
