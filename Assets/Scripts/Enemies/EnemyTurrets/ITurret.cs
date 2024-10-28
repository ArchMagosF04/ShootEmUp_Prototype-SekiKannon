using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret
{
    public bool IsShoting { get; set; }

    public void Shoot();
}
