using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurret : AbstractTurret
{
    protected List<Transform> barrels = new List<Transform>();

    protected BulletFactory factory;

    [SerializeField] protected float shootInterval = 0.4f;
    [SerializeField] protected int numberOfShoots = 3;

    [SerializeField] protected string ammoName;

    [SerializeField] protected bool aimsAtPlayer = false;
    protected Transform target;

    protected virtual void Awake()
    {
        barrels.AddRange(GetComponentsInChildren<Transform>());
        if (barrels.Contains(transform) && barrels.Count > 1)
        {
            barrels.Remove(transform);
        }

        factory = GetComponentInParent<BulletFactory>();
    }

    protected virtual void Start()
    {
        target = GameManager.Instance.PlayerCharacter.transform;
    }

    protected virtual Bullet_Controller CreateBullet(string bulletName, Transform t)
    {
        Bullet_Controller creation = factory.CreateBullet(bulletName);

        creation.transform.position = t.position;
        creation.transform.localRotation = t.rotation;

        return creation;
    }

    protected virtual void Aim(Bullet_Controller creation)
    {
        if (aimsAtPlayer && target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            creation.transform.up = direction;
            creation.Bullet_Movement.Movement(direction);
        }
        else
        {
            creation.Bullet_Movement.Movement(transform.up);
        }
    }
}
