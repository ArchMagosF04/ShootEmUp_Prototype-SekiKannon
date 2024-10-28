using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurret : BaseTurret
{
    [SerializeField] private int numberOfBulletsInArc;
    [SerializeField] private float arcAngle = 45f;

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
        float angleStep = (arcAngle * 2f) / (numberOfBulletsInArc-1);

        Quaternion initialRotation = transform.rotation;

        for (int i = 0; i < numberOfShoots; i++)
        {
            transform.rotation = initialRotation;

            transform.Rotate(0, 0, -arcAngle);

            foreach (Transform t in barrels)
            {
                for (int j = 0; j < numberOfBulletsInArc; j++)
                {
                    Bullet_Controller creation = CreateBullet(ammoName, t);

                    Aim(creation);

                    transform.Rotate(0,0, angleStep);
                }

            }
            yield return new WaitForSeconds(shootInterval);
        }

        transform.rotation = initialRotation;

        isShoting = false;
    }
}
