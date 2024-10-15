using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTurret : MonoBehaviour, ITurret
{
    [SerializeField] private int numberOfBulletsInRadius;

    private Rigidbody2D rb;

    [SerializeField] private List<Transform> barrels = new List<Transform>();

    private BulletFactory factory;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    [SerializeField] private string ammoName;

    private float radius = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        barrels.AddRange(GetComponentsInChildren<Transform>());

        if (barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }
        Quaternion f = Quaternion.identity;
        factory = GetComponentInParent<BulletFactory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
        float angleStep = 360 / numberOfBulletsInRadius;

        Quaternion initialRotation = transform.rotation;

        for (int i = 0; i < numberOfShoots; i++)
        {
            foreach (Transform t in barrels)
            {
                for (int j = 0; j < numberOfBulletsInRadius; j++)
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

    private IEnumerator AttackSequence2(string bulletName)
    {
        float angleStep = 360 / numberOfBulletsInRadius;
        float angle = 0;

        Quaternion initialRotation = transform.rotation;

        for (int i = 0; i < numberOfShoots; i++)
        {
            foreach (Transform t in barrels)
            {
                for (int j = 0; j < numberOfBulletsInRadius; j++)
                {
                    float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                    float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                    Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                    Vector2 projectileMoveDirection = (projectileVector - new Vector2(transform.position.x, transform.position.y)).normalized;

                    Bullet_Controller creation = factory.CreateBullet(bulletName, t);

                    creation.transform.position = t.position;
                    creation.transform.localRotation = t.rotation;

                    creation.Bullet_Movement.Movement(projectileMoveDirection);

                    angle += angleStep;
                }

            }
            yield return new WaitForSeconds(shootInterval);
        }

        transform.rotation = initialRotation;
    }
}
