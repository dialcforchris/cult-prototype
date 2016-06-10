using UnityEngine;
using System.Collections;

public class ProjectileShotgun : Projectile
{
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        SetProjectile(BulletType.SHOTGUN, 50, 500);

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
