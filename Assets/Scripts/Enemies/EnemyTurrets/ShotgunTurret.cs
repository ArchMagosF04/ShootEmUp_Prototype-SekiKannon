using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTurret : MonoBehaviour
{
    [SerializeField] private int numberOfBulletsInCone;
    [SerializeField] private float coneAngle = 45f;

    private Rigidbody2D rb;

    [SerializeField] private List<Transform> barrels = new List<Transform>();

    private BulletFactory factory;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    [SerializeField] private string ammoName;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        barrels.AddRange(GetComponentsInChildren<Transform>());

        if (barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }

        factory = GetComponentInParent<BulletFactory>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        StartCoroutine(AttackSequence(ammoName));
    }

    private IEnumerator AttackSequence(string bulletName)
    {
        float angleStep = (coneAngle*2) / numberOfBulletsInCone;

        Quaternion initialRotation = transform.rotation;

        for (int i = 0; i < numberOfShoots; i++)
        {
            transform.rotation = initialRotation;

            foreach (Transform t in barrels)
            {
                for (int j = 0; j < numberOfBulletsInCone; j++)
                {
                    Bullet_Controller creation = factory.CreateBullet(bulletName, t);

                    creation.transform.position = t.position;
                    creation.transform.localRotation = t.rotation;

                    //creation.Bullet_Movement.Movement(t.up);

                    transform.Rotate(0, 0, angleStep);
                }

            }
            yield return new WaitForSeconds(shootInterval);
        }

        transform.rotation = initialRotation;
    }
}
