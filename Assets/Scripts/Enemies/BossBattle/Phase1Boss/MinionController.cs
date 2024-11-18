using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [SerializeField] private AbstractTurret[] weaponList;
    private AbstractTurret currentWeapon;

    public bool isActive = false;

    private bool currentWeaponFired = true;

    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    private bool hasReachedTarget = true;

    private Transform currentTarget;
    private int listPointer = 0;

    [SerializeField] private float speed = 1.5f;

    private void Awake()
    {
        currentWeapon = weaponList[0];
    }

    private void Update()
    {
        if (isActive)
        {
            ShootRandomWeapon();
            Movement();
        }
    }

    private void ShootRandomWeapon()
    {
        if (currentWeaponFired && !currentWeapon.IsShoting)
        {
            int index = Random.Range(0, weaponList.Length);

            currentWeapon = weaponList[index];
            currentWeaponFired = false;
        }
        if (!currentWeaponFired)
        {
            currentWeaponFired = true;
            currentWeapon.Shoot();
        }
    }

    private void Movement()
    {
        if (hasReachedTarget)
        {
            currentTarget = waypoints[listPointer];

            listPointer++;
            if (listPointer == waypoints.Count)
            {
                listPointer = 0;
            }
            hasReachedTarget = false;
        }

        float distanceToTarget = (transform.position - currentTarget.position).magnitude;

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.fixedDeltaTime);

        if (distanceToTarget <= 0.1f)
        {
            hasReachedTarget = true;
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
