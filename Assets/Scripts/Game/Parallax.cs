using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght;
    private float startPosition;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPosition = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        float distance = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if (temp > startPosition + lenght)
        {
            startPosition += lenght;
        }
        else if (temp < startPosition - lenght) 
        {
            startPosition -= lenght;
        }
    }
}
