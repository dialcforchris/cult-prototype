using UnityEngine;
using System.Collections;
using DG.Tweening;

public abstract class Actor : MonoBehaviour
{

    int health;
    public float speed;
    public Weapon currentWeapon;
    public string weaponName = "Punch";
    public WeaponFists mits;
    public WeaponGun shooter;
    public WeaponMelee stabby;
    public Rigidbody2D body;
    float deathCounter = 10;
    ActorState state;
    public Animator animator;
    WeaponSelect theWeapon;
    bool attacking = false;
    float meleeTime = 0;
    // Use this for initialization
    public virtual void Awake()
    {
        currentWeapon = mits;
        theWeapon = WeaponSelect.FISTS;
        state = ActorState.ALIVE;
        body = GetComponent<Rigidbody2D>();
    }
    public void SetHealth(int _health)
    {
        health = _health;
    }
    // Update is called once per frame
   public virtual void Update()
    {
        CheckAmmo();
        weaponName = currentWeapon.name.ToString();
        Death();
        Movement();
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }
    public void Death()
    {
        if (health <= 0)
        {
            state = ActorState.DEAD;
            animator.SetBool("Dead",true);
            GetComponent<Collider2D>().enabled = false;
        }
    }
    public void FadeAtDeath()
    {
        if (state == ActorState.DEAD)
        {
            deathCounter -= Time.deltaTime;
        }
        if (deathCounter <= 0)
        {
           Tweener death = GetComponent<SpriteRenderer>().DOFade(0, 2);
           death.OnComplete(() =>
           {
               Destroy(gameObject);
           });
        }
    }
    public void Suicide()
    {
        animator.SetBool("Suicide", true);
    }
  
    void DeadFadeOut()
    {
        SpriteRenderer actorSprite = gameObject.GetComponent<SpriteRenderer>();
        actorSprite.DOFade(0, 0.8f);
    }
   public virtual void Attack(string type)
    {
       currentWeapon.Attack(type);
    }
   public void Melee()
    {
       animator.SetTrigger("Melee");
       attacking = true;
    }
    public void Punch()
   {
       animator.SetTrigger("Punch");
       attacking = true;
   }
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }
    protected virtual void Movement()
    { }
      
   public void setWeapon()
    {
       if (Input.GetKeyDown(KeyCode.R))
       {
           currentWeapon = mits;
        //   weaponName = currentWeapon.name.ToString();
       }
       else if(Input.GetKeyDown(KeyCode.E))
       {
           currentWeapon = stabby;
         //  weaponName = currentWeapon.name.ToString();
       }
       else if (Input.GetKeyDown(KeyCode.T))
       {
           shooter.SetGunType(GunType.HANDGUN);
           currentWeapon = shooter;
        //   weaponName = currentWeapon.name.ToString();
       }
       else if (Input.GetKeyDown(KeyCode.U))
       {
           shooter.SetGunType(GunType.SHOTGUN);
           currentWeapon = shooter;
        //   weaponName = currentWeapon.name.ToString();
       }
       else if (Input.GetKeyDown(KeyCode.Y))
       {
           shooter.SetGunType(GunType.MACHINEGUN);
           currentWeapon = shooter;
       }
    }
    public void CheckAmmo()
   {
        if (currentWeapon.GetAmmo()==0 && !currentWeapon.IsMelee())
        {
            currentWeapon = mits;
        }
   }
    public ActorState ReturnState()
    {
        return state;
    }
}
public enum ActorState
{
    ALIVE,
    DEAD,
    IDLE
}
public enum WeaponSelect
{
    FISTS,
    MELEE,
    GUN,
}