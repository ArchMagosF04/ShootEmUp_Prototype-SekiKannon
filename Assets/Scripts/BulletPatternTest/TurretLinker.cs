using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLinker : MonoBehaviour, ITurret
{
    [SerializeField] private ITurret[] turrets;

    public void Shoot()
    {
        foreach (var t in turrets)
        {
            t.Shoot();
        }
    }
}
