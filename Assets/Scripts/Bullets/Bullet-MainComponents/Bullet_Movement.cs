using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet_Movement : MonoBehaviour
{
    private float speedMultiplier = 1f;

    private Rigidbody2D rb;
    private Bullet_Controller controller;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Bullet_Controller>();
    }

    private void OnEnable()
    {
        speedMultiplier = 1f;
    }

    public void MultiplySpeed(float modifier)
    {
        speedMultiplier = modifier;
    }

    public void Movement(Vector2 direction)
    {
        rb.velocity = direction * controller.BulletData.Speed * speedMultiplier;
    }

    public void AssignMovement(Vector2 velocity)
    {
        rb.velocity = (Vector3)velocity;
    }
}
