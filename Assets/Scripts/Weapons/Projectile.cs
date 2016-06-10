using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    GameObject parent;
    int damage;
    float maxAlive;
    float aliveTime = 2;
    float speed;
    public Vector2 direction;
    public BulletType bType;
    public float weaponCoolDown;
    Rigidbody2D body;
    
    // Use this for initialization
	public virtual void Start () 
    {
        body = GetComponent<Rigidbody2D>();
        
   	}
	public void SetParent(GameObject _parent)
    {
        parent = _parent;
    }
    void Alive()
    {
        aliveTime -= Time.deltaTime;
        if (aliveTime<=0)
        {
            DeactivateProj();
        }
    }
	// Update is called once per frame
   public virtual void Update()
    {

        body.velocity = transform.up * (speed * Time.deltaTime);
        Alive();
    }

   public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != parent)
        {
            if (col.GetComponent<Actor>())
            {
                col.gameObject.GetComponent<Actor>().TakeDamage(damage);
                BloodManager.instance.PlayBlood(col.gameObject.transform.position, transform.position);
                DeactivateProj();
            }
            else
            {
                DeactivateProj();
            }
        }
    }
    void DeactivateProj()
    {
        aliveTime = maxAlive;
        gameObject.SetActive(false);
        gameObject.GetComponent<Projectile>().enabled = false;
        
    }
    public void SetProjectile(BulletType _type, int _damage, float _speed = 500,float _aliveTime = 2)
    {
        bType = _type;
        damage = _damage;
        maxAlive = _aliveTime;
        speed = _speed;
    }
}
public enum BulletType
{
    HANDGUN,
    SHOTGUN,
    MACHINEGUN,
}