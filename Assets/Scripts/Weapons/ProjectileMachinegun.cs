using UnityEngine;
using System.Collections;

public class ProjectileMachinegun : Projectile
{

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        SetProjectile(BulletType.MACHINEGUN, 10, 700);

    }
    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
    }
    public override void Update()
    {
        base.Update();
    }
}
