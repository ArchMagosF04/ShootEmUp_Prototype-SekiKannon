using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StraightShotTurret : BaseTurret
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    protected override void Shoot()
    {
        StartCoroutine(AttackSequence());
    }

    protected override IEnumerator AttackSequence()
    {
        for (int i = 0; i < numberOfShoots; i++)
        {
            foreach (Transform t in barrels)
            {
                Bullet_Controller creation = CreateBullet(ammoName, t);

                AimAtPlayer(creation);
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
