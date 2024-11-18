using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelBullet : MonoBehaviour, IBulletDeathEffect
{
    private AbstractTurret shrapnelPattern;

    private void Awake()
    {
        shrapnelPattern = GetComponent<AbstractTurret>();
    }

    public void OnDeathEffect()
    {
        shrapnelPattern.Shoot();
    }
}
