using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlueBulletData", menuName = "Scriptable Objects/Bullets/OrangeBulletData", order = 3)]
public class OrangeBulletSO : ScriptableObject
{
    [field: SerializeField] public int ShieldDamage { get; private set; }
    [field: SerializeField, Range(1.5f, 10f)] public float SpeedOnDeflect { get; private set; }
    [field: SerializeField, Range(1.55f, 5)] public float DamageOnDeflect { get; private set; }
    [field: SerializeField] public LayerMask whatDestroysBulletWhenDeflected { get; private set; }
}
