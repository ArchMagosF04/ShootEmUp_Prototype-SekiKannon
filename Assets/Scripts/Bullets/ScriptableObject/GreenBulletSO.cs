using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlueBulletData", menuName = "Scriptable Objects/Bullets/GreenBulletData", order = 2)]
public class GreenBulletSO : ScriptableObject
{
    [field: SerializeField] public int ShieldDamage { get; private set; }
    [field: SerializeField] public int BlockHeal { get; private set; }
    [field: SerializeField] public int ParryHeal { get; private set; }
}
