using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Bullet_Movement bullet_Movement;

    private Transform target;

    private Rigidbody2D rb;

    [SerializeField] private float rotationSpeed = 1000f;

    private void Awake()
    {
        bullet_Movement = GetComponent<Bullet_Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.PlayerCharacter.transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotationSpeed;

            bullet_Movement.Movement(transform.up);
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
