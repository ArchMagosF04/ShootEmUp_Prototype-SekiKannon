using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MineBullet : MonoBehaviour
{
    private Bullet_Movement bullet_Movement;

    private Transform target;

    private Rigidbody2D rb;

    private Animator anim;

    private bool isMineActive;
    private bool playerDetected;

    [SerializeField] private float activationDistance = 2.3f;

    [SerializeField] private float rotationSpeed = 1000f;

    [SerializeField] private float activationDelay = 0.35f;

    [SerializeField] private SoundLibraryObject soundLibrary;

    private void Awake()
    {
        bullet_Movement = GetComponent<Bullet_Movement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        isMineActive = false;
        playerDetected = false;

        ChangeTarget(GameManager.Instance.PlayerCharacter.transform);
    }

    private void Update()
    {
        if (!isMineActive)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= activationDistance && !playerDetected)
            {
                anim.SetBool("IsActive", true);
                playerDetected = true;

                bullet_Movement.AssignMovement(new Vector2(0f, 0f));
                SoundManager.Instance.CreateSound().WithSoundData(soundLibrary.soundData[0]).Play();

                Invoke("ActivateMine", activationDelay);
            }
        }
    }

    private void ActivateMine()
    {
        isMineActive = true;
    }

    private void FixedUpdate()
    {
        if (target != null && isMineActive)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
