using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float Speed => speed;

    private Rigidbody2D rb; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //Movement(transform.up);
    }

    private void FixedUpdate()
    {
        //rb.velocity = transform.up * speed;
    }

    public void ModifySpeed(float speed)
    {
        this.speed = speed;
    }

    public void Movement(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
}
