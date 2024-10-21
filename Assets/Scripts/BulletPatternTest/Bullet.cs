using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float maxLifeTime = 5f;
    private float lifeTime = 0f;

    public Vector2 Velocity;

    private void Update()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;
        lifeTime += Time.deltaTime;

        if (lifeTime > maxLifeTime)
        {
            Disable();
        }
    }

    private void Disable()
    {
        lifeTime = 0f;
        gameObject.SetActive(false);
    }
}
