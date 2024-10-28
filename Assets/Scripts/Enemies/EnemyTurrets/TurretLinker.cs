using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLinker : BaseTurret
{
    [SerializeField] public List<AbstractTurret> turrets = new List<AbstractTurret>();

    private bool isShoting = false;
    public override bool IsShoting { get => isShoting; set => isShoting = value; }

    protected override void Awake()
    {
        turrets.AddRange(GetComponentsInChildren<AbstractTurret>());
        if (turrets.Contains(this))
        {
            turrets.Remove(this);
        }
    }

    public override void Shoot()
    {
        foreach (var t in turrets)
        {
            t.Shoot();
        }
    }
}
