using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewBulletData", menuName = "Scriptable Objects/Bullets/BulletData", order = 0)]
public class ScriptableBullet : ScriptableObject
{
    [SerializeField] public float initialLifeTime;
    [SerializeField] public float normalSpeed;
    [SerializeField] public int normalDamage;
    [SerializeField] public Animator animator;
}
