using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    protected List<Transform> barrels = new List<Transform>();

    protected BulletFactory factory;

    [SerializeField] protected float shootInterval = 0.4f;
    [SerializeField] protected int numberOfShoots = 3;

    [SerializeField] protected string ammoName;

    [SerializeField] protected bool aimsAtPlayer = false;
    protected Transform target;

    protected bool isAttacking = false;

    [SerializeField] protected bool shouldRotate = false;
    [SerializeField] protected float startingAngle;
    [SerializeField] protected float endAngle;

    protected virtual void Awake()
    {
        barrels.AddRange(GetComponentsInChildren<Transform>());
        if (barrels.Contains(transform))
        {
            barrels.Remove(transform);
        }

        factory = GetComponentInParent<BulletFactory>();
    }

    protected virtual void Start()
    {
        target = GameManager.Instance.PlayerCharacter.transform;
    }

    protected abstract void Shoot();

    protected abstract IEnumerator AttackSequence();

    protected virtual Bullet_Controller CreateBullet(string bulletName, Transform t)
    {
        Bullet_Controller creation = factory.CreateBullet(bulletName, t);

        creation.transform.position = t.position;
        creation.transform.localRotation = t.rotation;

        return creation;
    }

    protected virtual void AimAtPlayer(Bullet_Controller creation)
    {
        if (aimsAtPlayer && target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            creation.transform.up = direction;
            creation.Bullet_Movement.Movement(direction);
        }
    }

    protected virtual void RotateTurret(float buffer)
    {

    }
}
