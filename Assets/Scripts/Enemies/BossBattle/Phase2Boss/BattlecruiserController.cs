using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattlecruiserController : MonoBehaviour
{
    [SerializeField] private AbstractTurret shrapnelWeapon;
    [SerializeField] private AbstractTurret sprayWeapon;
    [SerializeField] private AbstractTurret missileWeapon;

    private bool inPosition = false;
    private bool isDeafeted = false;

    private Transform mainWaypoint;

    private EnemyHealth health;

    private Animator anim;
    [SerializeField] private SpriteRenderer engineSprite;

    private EnemyMovement movement;

    public event Action OnEnemyDeath;

    private void Awake()
    {
        GameManager.Instance.SetBossReference(this.gameObject);
        mainWaypoint = GameObject.FindGameObjectWithTag("BattlecruiserPosition").transform;
        health = GetComponent<EnemyHealth>();
        movement = GetComponent<EnemyMovement>();

        anim = GetComponentInChildren<Animator>();

        health.OnEnemyDeath += DeathAnimation;
    }

    private void Update()
    {
        if (!isDeafeted)
        {
            if (inPosition)
            {
                ShootingLogic();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!inPosition)
        {
            MoveTowards();
        }
    }

    private void MoveTowards()
    {
        float distanceToTarget = (transform.position - mainWaypoint.position).magnitude;

        transform.position = Vector3.MoveTowards(transform.position, mainWaypoint.position, 3f * Time.fixedDeltaTime);

        if (distanceToTarget <= 0.1f)
        {
            inPosition = true;
            movement.SetSpeedMultiplier(1f);
        }
    }

    private void DeathAnimation()
    {
        health.OnEnemyDeath -= DeathAnimation;
        movement.SetSpeedMultiplier(0f);

        isDeafeted = true;
        Destroy(engineSprite);
        Destroy(shrapnelWeapon.gameObject);
        Destroy(sprayWeapon.gameObject);
        Destroy(missileWeapon.gameObject);
        anim.SetBool("IsDead", true);

        Invoke("DeathEvent", 1.4f);
    }

    private void DeathEvent()
    {
        OnEnemyDeath?.Invoke();
    }

    private void ShootingLogic()
    {
        shrapnelWeapon.Shoot();
        sprayWeapon.Shoot();
        missileWeapon.Shoot();
    }


    private void OnDisable()
    {
        if (health != null)
        {
            health.OnEnemyDeath -= DeathAnimation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            damageable.TakeDamage(5);
        }
    }
}
