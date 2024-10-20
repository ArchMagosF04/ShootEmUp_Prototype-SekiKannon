using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotator : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float rotateAmount = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = -rotateAmount * rotationSpeed;
    }
}
