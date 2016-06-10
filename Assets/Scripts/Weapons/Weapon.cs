using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
    int damage;
    int clipSize;
    public int ammo;
    float force;
    bool melee;
    public float cooldown;
    public float currentCooldown;
    public  bool attacking = false;
    public float meleeCool;
    public Actor actor;
    public Collider2D hitBox = null;
    public ParticleSystem blood;
    void Awake()
    {
      
    }
   public virtual void Update()
    {
        if (hitBox!=null)
        {
            hitBox.enabled = attacking;
        }
        CoolDown();
       
    }
    public float GetForce()
   {
       return force;
   }
    public void Reload()
    {
        ammo = clipSize;
    }
   public void SetWeapon(int _damage, float _cooldown, bool _melee, int _clipSize, int _ammo,float _force)
    {
        damage = _damage;
        cooldown = _cooldown;
        melee = _melee;
        clipSize = _clipSize;
        ammo = _ammo;
        force = _force;
    }
    public virtual void Attack(string type)
    {
        if (currentCooldown >= cooldown && !hitBox.enabled)
        {
                attacking = true;
                currentCooldown = 0;
                actor.animator.SetTrigger(type);
        }
    }
    public bool IsMelee()
    {
        return melee;
    }
    void CoolDown()
    {
        if (currentCooldown < cooldown)
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= meleeCool)
        {
            attacking = false;
        }
        }
       
        
    }
    public void UseAmmo()
    {
        ammo --;
    }
    public int GetAmmo()
    {
        return ammo;
    }
    public int GetClipSize()
    {
        return clipSize;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.GetComponent<Actor>())
        {
            BloodManager.instance.PlayBlood(col.gameObject.transform.position, actor.transform.position);
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(-actor.transform.position*200);
            col.gameObject.GetComponent<Actor>().TakeDamage(damage);
        }
    }
}
