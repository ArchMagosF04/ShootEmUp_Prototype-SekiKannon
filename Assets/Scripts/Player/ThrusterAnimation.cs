using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake() 
    {
        anim = GetComponent<Animator>(); 
        rb = GetComponentInParent<Rigidbody2D>(); 
    }

    private void Update()
    {
        if (rb.velocity == Vector2.zero)   //If the ship has no velocity, then it is not moving.
        {
            anim.SetBool("IsMoving", false);
        }
        else
        {
            anim.SetBool("IsMoving", true);
        }
    }
}
