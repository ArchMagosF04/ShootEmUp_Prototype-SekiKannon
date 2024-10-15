using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmartBullet : MonoBehaviour
{
    private Bullet_Movement bullet_Movement;

    private Transform target;

    private Rigidbody2D rb;

    private void Awake()
    {
        bullet_Movement = GetComponent<Bullet_Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (target != null)
        {
            transform.up = (target.position - transform.position).normalized;
            bullet_Movement.Movement(transform.up);
        }
    }
}
