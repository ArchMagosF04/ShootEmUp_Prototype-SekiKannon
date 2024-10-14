using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightShotTurret : MonoBehaviour, ITurret
{
    [SerializeField] private List<Transform> barrels = new List<Transform>();

    private BulletFactory factory;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    [SerializeField] private string ammoName;

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
        if (Input.GetMouseButtonDown(0))
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
        for (int i = 0; i < numberOfShoots; i++)
        {
            foreach (Transform t in barrels)
            {
                Bullet_Controller creation = factory.CreateBullet(bulletName, t);

                creation.transform.position = t.position;
                creation.transform.localRotation = t.rotation;

                creation.Bullet_Movement.Movement(t.up);
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
