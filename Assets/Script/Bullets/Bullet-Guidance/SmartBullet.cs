using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmartBullet : MonoBehaviour
{
    private Bullet_Movement bullet_Movement;

    private Transform target;

    private void Awake()
    {
        bullet_Movement = GetComponent<Bullet_Movement>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        if (target != null)
        {
            transform.up = (target.position - transform.position).normalized;
            bullet_Movement.Movement(transform.up);
        }
    }
}
