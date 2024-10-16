using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBlueBulletData", menuName = "Scriptable Objects/Bullets/DefaultColorBulletData", order = 1)]
public class DefaultColorBulletSO : ScriptableObject
{
    [field: SerializeField] public int ShieldDamage { get; private set; }
}
