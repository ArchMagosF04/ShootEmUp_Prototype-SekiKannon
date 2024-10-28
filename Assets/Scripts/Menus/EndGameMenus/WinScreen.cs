using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
   [SerializeField] private Button home;
    [SerializeField] private Image homeIcon;
    [SerializeField] private Image fade;
   [SerializeField] private Image winIcon;

    void Start()
    {
        homeIcon.enabled = false;
        winIcon.enabled = false;
        fade.enabled = false;
    }

    void Update()
    {
        
    }
}
