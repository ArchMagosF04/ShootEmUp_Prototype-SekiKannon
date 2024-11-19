using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayingBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float changeTime = 0.5f;
    private float timer;

    [SerializeField] private AnimationCurve speedCurve;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (timer >= changeTime)
        {
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, speedCurve.Evaluate(timer));
    }
}
