using UnityEngine;
using System.Collections;

public class WeaponGun : Weapon 
{
    public Projectile bullet;
    //public ProjectileHandgun hand;
    //public ProjectileMachinegun mach;
    //public ProjectileShotgun shot;

   public GunType gType;
   public GunType prevType = GunType.NONE;
	// Use this for initialization
	public virtual void Start () 
    {
         SetGunType(GunType.HANDGUN);
         SetWeapon(0, 0.5f, false, 12, 12, 50);
   	}
	
	// Update is called once per frame
	public override void Update () 
    {
       base.Update();
  	}
    public override void Attack(string type)
    {
        if (currentCooldown >= cooldown)//&& !attacking)
        {
            attacking = true;
            currentCooldown = 0;
            actor.animator.SetTrigger(type);
            Projectile b = ProjectileManager.instance.PoolingProjectiles(bullet);
            TypeSwitch(b, gType);
            b.gameObject.SetActive(true);
            b.SetParent(actor.gameObject);
            b.transform.position = transform.position;
            b.direction = actor.transform.forward.normalized;
            b.transform.rotation = actor.transform.rotation;
            UseAmmo();
        }
    }
  
    public void SetGunType(GunType _gType)
    {
        switch (_gType)
        {
            case GunType.HANDGUN:
                {
                    SetWeapon(0, 0.5f, false, 12, 12, 0);
                    break;
                }
            case GunType.MACHINEGUN:
                {
                    SetWeapon(0, 0.3f, false, 30, 30, 0);
                    break;
                }
            case GunType.SHOTGUN:
                {
                    SetWeapon(0, 0.8f, false, 8, 8, 0);
                    break;
                }
        }
        gType = _gType;
    }
    void TypeSwitch(Projectile _b,GunType _type)
    {
       // type = _type;
        switch (_type)
        {
            case GunType.HANDGUN:
                _b.SetProjectile(BulletType.HANDGUN, 10);
              //  cooldown = 0.5f;
                break;
            case GunType.MACHINEGUN:
                 _b.SetProjectile(BulletType.MACHINEGUN, 10, 700);
                //cooldown = 0.3f;
                break;
            case GunType.SHOTGUN:
                _b.SetProjectile(BulletType.SHOTGUN, 20, 400);
                 //cooldown = 0.8f;
                 break;
        }
       
    }
    
    
}
public enum GunType
{
    HANDGUN,
    SHOTGUN,
    MACHINEGUN,
    NONE,
}