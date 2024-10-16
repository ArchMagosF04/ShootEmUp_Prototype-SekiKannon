using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StraightShotTurret : MonoBehaviour, ITurret
{
    [SerializeField] private List<Transform> barrels = new List<Transform>();

    private BulletFactory factory;

    [SerializeField] private float shootInterval = 0.4f;
    [SerializeField] private int numberOfShoots = 3;

    [SerializeField] private string ammoName;

    [SerializeField] private bool aimsAtPlayer = false;
    private Transform target;

    private void Awake()
    {
        barrels.AddRange(GetComponentsInChildren<Transform>());

        if (barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }

        factory = GetComponentInParent<BulletFactory>();
    }

    private void Start()
    {
        target = GameManager.Instance.PlayerCharacter.transform;
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

                if (aimsAtPlayer)
                {
                    if (target != null)
                    {
                        Vector2 direction = (target.position - transform.position).normalized;
                        creation.transform.up = direction;
                        creation.Bullet_Movement.Movement(direction);
                    }
                }
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
