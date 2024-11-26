using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLinker : AbstractTurret
{
    [SerializeField] public List<AbstractTurret> turrets = new List<AbstractTurret>();

    private bool isShoting = false;
    public override bool IsShoting { get => isShoting; set => isShoting = value; }

    private void Awake()
    {
        turrets.AddRange(GetComponentsInChildren<AbstractTurret>());
        if (turrets.Contains(this))
        {
            turrets.Remove(this);
        }
    }

    private void Update()
    {
        if (isShoting)
        {
            foreach (var t in turrets)
            {
                if (t.IsShoting) return;
            }

            isShoting = false;
        }
    }

    public override void Shoot()
    {
        if (!isShoting)
        {
            isShoting = true;

            foreach (var t in turrets)
            {
                t.Shoot();
            }
        }
    }
}
