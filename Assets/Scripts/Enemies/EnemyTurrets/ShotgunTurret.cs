using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurret : MonoBehaviour
{
    [SerializeField] private int numberOfBulletsInArc;
    [SerializeField] private float arcAngle = 45f;

    [SerializeField] private List<Transform> barrels = new List<Transform>();

    private BulletFactory factory;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    [SerializeField] private string ammoName;

    private bool isAttacking = false;

    [SerializeField] private float shootCooldown = 5f;
    private float timer = 0f;

    private void Awake()
    {
        barrels.AddRange(GetComponentsInChildren<Transform>());

        if (barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }

        factory = GetComponentInParent<BulletFactory>();
    }

    public void Update()
    {
        if (timer <= 0f)
        {
            Shoot();
            timer = shootCooldown;
        }

        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackSequence(ammoName));
        }
    }

    private IEnumerator AttackSequence(string bulletName)
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
                    Bullet_Controller creation = factory.CreateBullet(bulletName); //CHECKTHIS

                    creation.transform.position = t.position;
                    creation.transform.localRotation = t.rotation;

                    transform.Rotate(0,0, angleStep);
                }

            }
            yield return new WaitForSeconds(shootInterval);
        }

        transform.rotation = initialRotation;

        isAttacking = false;
    }
}
