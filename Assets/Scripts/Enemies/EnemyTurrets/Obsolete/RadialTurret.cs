using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTurret : MonoBehaviour
{
    [SerializeField] private int numberOfBulletsInRadius;

    [SerializeField] private List<Transform> barrels = new List<Transform>();

    private BulletFactory factory;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    [SerializeField] private string ammoName;

    private float radius = 5f;

    private bool isAttacking = false;

    private void Awake()
    {
        barrels.AddRange(GetComponentsInChildren<Transform>());

        if (barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }

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
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackSequence(ammoName));
        }
        
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
                    Bullet_Controller creation = factory.CreateBullet(bulletName); //CHECKTHIS

                    creation.transform.position = t.position;
                    creation.transform.localRotation = t.rotation;

                    transform.Rotate(0, 0, angleStep);
                }
                
            }
            yield return new WaitForSeconds(shootInterval);
        }

        transform.rotation = initialRotation;

        isAttacking = false;
    }

    private IEnumerator AttackSequence2(string bulletName)
    {
        float angleStep = 360 / numberOfBulletsInRadius;
        float angle = 0;

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

                    Bullet_Controller creation = factory.CreateBullet(bulletName); //CHECKTHIS

                    creation.transform.position = t.position;
                    creation.transform.localRotation = t.rotation;

                    creation.transform.up = projectileMoveDirection;

                    creation.Bullet_Movement.Movement(creation.transform.up);

                    angle += angleStep;
                }

            }
            yield return new WaitForSeconds(shootInterval);
        }
        isAttacking = false;
    }
}
