using UnityEngine;
using System.Collections;

public class ProjectileHandgun : Projectile {

	// Use this for initialization
    public override void Start()
    {
        base.Start();
        SetProjectile(BulletType.HANDGUN, 10, 500);

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
