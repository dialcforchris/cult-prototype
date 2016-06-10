using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Actor
{
    public static Player instance = null;
   // SpriteRenderer spRend;
    Ray2D rayCast;
    RaycastHit2D hit;
    int followers;
    List<Follower> followerList = new List<Follower>();
    public override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        base.Awake();
    }
    // Use this for initialization
	void Start ()
    {
        //speed = 100;/// *Time.deltaTime;
        body = GetComponent<Rigidbody2D>();
     //   spRend = GetComponent<SpriteRenderer>();
        SetHealth(5550);
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Update();
        setWeapon();
        if (Input.GetButton("Fire"))
        {
            Attack(weaponName);
        }
	}
    protected override void Movement()
    {
         Vector2 moveDirection = (new Vector2(Input.GetAxis("Horizontal")*speed, Input.GetAxis("Vertical")*speed));
        body.velocity = moveDirection;
        animator.SetBool("Walking", moveDirection != Vector2.zero ? true : false);
     
        if (moveDirection != Vector2.zero)
        {
           float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
           transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
    }
   public int CurrentFollowers()
    {
        return followers;
    }
    public void AddFollowers(int _followers,Follower _member)
    {
        for (int i=0;i<_followers;i++)
        {
            followerList.Add(Instantiate(_member));
        }
        followers = followerList.Count;
    }
}
