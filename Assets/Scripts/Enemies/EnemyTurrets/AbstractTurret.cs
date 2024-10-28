using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTurret : MonoBehaviour
{
    public abstract bool IsShoting { get; set; }

    public abstract void Shoot();
}
