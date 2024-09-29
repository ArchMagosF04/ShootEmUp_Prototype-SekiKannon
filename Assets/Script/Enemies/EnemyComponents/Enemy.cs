using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 10f;
    private float currentHealth;

    private Rigidbody2D rb;

    [SerializeField] private float speed = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        Movement();
    }

    private void Update()
    {
        
    }

    private void Movement()
    {
        rb.velocity = Vector2.right * speed;
    }

    public void TakeDamage(float damageReceived)
    {
        currentHealth -= damageReceived;
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Destroy(this.gameObject);
        }
    }
}
