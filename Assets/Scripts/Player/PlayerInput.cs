using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Key Inputs")]
    [SerializeField] private KeyCode shoot = KeyCode.X;  //Input use to shoot projectiles.
    [SerializeField] private KeyCode shield = KeyCode.Z; //Input use to activate the shield.

    private Vector2 inputVector;
    public Vector2 InputVector => inputVector;

    private bool isShotting;
    public bool IsShooting => isShotting;

    private bool isShieldActive;
    public bool IsShieldActive => isShieldActive;

    private void Update()
    {
        ReceiveInputs();
    }

    private void ReceiveInputs() //Detects the inputs.
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(inputX, inputY).normalized;

        isShotting = Input.GetKey(shoot);
        isShieldActive = Input.GetKey(shield);
    }
}