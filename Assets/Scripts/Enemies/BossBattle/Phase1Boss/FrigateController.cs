using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrigateController : MonoBehaviour
{
    [SerializeField] private AbstractTurret weapon;

    [SerializeField] private MinionController[] minionControllers;

    private bool inPosition = false;
    private bool isDeafeted = false;

    private Transform mainWaypoint;

    private void Awake()
    {
        GameManager.Instance.SetBossReference(this.gameObject);
        mainWaypoint = GameObject.FindGameObjectWithTag("FrigatePosition").transform;

        minionControllers = GetComponentsInChildren<MinionController>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            damageable.TakeDamage(5);
        }
    }
}
