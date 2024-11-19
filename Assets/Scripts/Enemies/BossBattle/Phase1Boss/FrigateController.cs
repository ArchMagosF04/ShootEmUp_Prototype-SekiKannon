using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrigateController : MonoBehaviour
{
    [SerializeField] private AbstractTurret weapon;

    [SerializeField] private MinionController[] minionControllers;

    private bool inPosition = false;
    private bool isDeafeted = false;

    private Transform mainWaypoint;

    private EnemyHealth health;

    private Animator anim;
    [SerializeField] private SpriteRenderer engineSprite;

    public event Action OnEnemyDeath;

    private void Awake()
    {
        GameManager.Instance.SetBossReference(this.gameObject);
        mainWaypoint = GameObject.FindGameObjectWithTag("FrigatePosition").transform;
        health = GetComponent<EnemyHealth>();

        minionControllers = GetComponentsInChildren<MinionController>();
        anim = GetComponentInChildren<Animator>();

        health.OnEnemyDeath += DeathAnimation;
    }

    private void Update()
    {
        if (!isDeafeted)
        {
            if (!inPosition)
            {
                MoveTowards();
            }
            else
            {
                weapon.Shoot();
            }
        }
    }

    private void MoveTowards()
    {
        float distanceToTarget = (transform.position - mainWaypoint.position).magnitude;

        transform.position = Vector3.MoveTowards(transform.position, mainWaypoint.position, 1f * Time.fixedDeltaTime);

        if (distanceToTarget <= 0.1f)
        {
            inPosition = true;

            Invoke("ActivateMinions", 0.5f);
        }
    }

    private void ActivateMinions()
    {
        foreach(MinionController bomber in minionControllers)
        {
            bomber.isActive = true;
        }
    }

    private void DeathAnimation()
    {
        health.OnEnemyDeath -= DeathAnimation;

        isDeafeted = true;
        Destroy(engineSprite);
        anim.SetBool("IsDead", true);
        foreach (MinionController bomber in minionControllers)
        {
            bomber.DeathAnimation();
        }

        Invoke("DeathEvent", 0.55f);
    }

    private void DeathEvent()
    {
        OnEnemyDeath?.Invoke();
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
