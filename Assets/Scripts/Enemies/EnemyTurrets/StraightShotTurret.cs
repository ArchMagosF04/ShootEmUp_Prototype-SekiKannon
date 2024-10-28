using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StraightShotTurret : BaseTurret
{
    //[SerializeField] private float shootCooldown = 2f;
    //private float timer = 0f;

    private bool isShoting = false;
    public override bool IsShoting { get => isShoting; set => isShoting = value; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    //public void Update()
    //{
    //    if (timer <= 0f)
    //    {
    //        Shoot();
    //        timer = shootCooldown;
    //    }

    //    timer -= Time.deltaTime;
    //}

    public override void Shoot()
    {
        if (!isShoting)
        {
            isShoting = true;
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        for (int i = 0; i < numberOfShoots; i++)
        {
            foreach (Transform t in barrels)
            {
                Bullet_Controller creation = CreateBullet(ammoName, t);

                Aim(creation);
            }
            yield return new WaitForSeconds(shootInterval);
        }

        isShoting = false;
    }
}
