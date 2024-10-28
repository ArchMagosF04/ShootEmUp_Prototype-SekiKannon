using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool scrollLeft;

    private float singleTextureWidth;
    private float startPosition;

    private void Start()
    {
        startPosition = transform.position.x;
        singleTextureWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        if (scrollLeft)
        {
            moveSpeed = - moveSpeed;
        }
    }

    private void Update()
    {
        Scroll();
        CheckReset();
    }

    private void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void CheckReset()
    {
        if ((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > singleTextureWidth)
        {
            transform.position = Vector3.zero;
        }
    }
}
