using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatingBullet : MonoBehaviour
{
    [SerializeField] private float amplitude = 2f; // The distance the bullet will oscillate.
    [SerializeField] private float frequency = 1f; // The speed of oscillation.
    [SerializeField] private bool verticalOscillation = true;

    private Vector3 startPosition; // Starting position of the bullet.
    private float startTime; // Time the bullet starts moving.

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    { 
        // Store the bullet's initial position at the start.
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // Create the oscillating motion using sine wave for horizontal movement.
        float xOffset = Mathf.Sin((Time.time - startTime) * frequency) * amplitude;

        // Apply oscillation to the bullet's position.

        if (verticalOscillation)
        {
            rb.velocity = new Vector2(rb.velocity.x, xOffset);
            //transform.position = new Vector3(startPosition.x, transform.position.y + xOffset, transform.position.z);
        }
        else
        {
            rb.velocity = new Vector2(xOffset , rb.velocity.y);
            //transform.position = new Vector3(startPosition.x + xOffset, transform.position.y, transform.position.z);
        }

        
    }
}
