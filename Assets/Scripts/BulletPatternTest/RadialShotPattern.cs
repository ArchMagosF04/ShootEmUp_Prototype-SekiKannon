using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletHell System/Radial Shot Pattern")]
public class RadialShotPattern : ScriptableObject
{
    public int Repetitions;
    public RadialShotSettings[] PatternSettings;
}
