using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelBullet : MonoBehaviour, IBulletDeathEffect
{
    [SerializeField] private AbstractTurret shrapnelPattern;
    [SerializeField] private float shrapnelDuration = 1f;

    public void OnDeathEffect()
    {
        AbstractTurret turret = Instantiate(shrapnelPattern, transform.position, transform.rotation);
        turret.Shoot();
        Destroy(turret, shrapnelDuration);
    }
}
