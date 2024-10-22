using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private List<Transform> waypoints = new List<Transform>();

    private bool hasReachedTarget = true;

    private Transform currentTarget;
    private int listPointer = 0;

    [SerializeField] private float speed = 3f;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject waypointHolder = GameObject.FindGameObjectWithTag("BossWaypoint");
        waypoints.AddRange(waypointHolder.GetComponentsInChildren<Transform>());

        currentSpeed = speed;

        GameManager.Instance.SetBossReference(gameObject);
    }

    private void FixedUpdate()
    {
        if (waypoints.Count > 1)
        {
            Movement();
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

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, currentSpeed * Time.fixedDeltaTime);

        if (distanceToTarget <= 0.1f)
        {
            hasReachedTarget = true;
        }
    }
}
