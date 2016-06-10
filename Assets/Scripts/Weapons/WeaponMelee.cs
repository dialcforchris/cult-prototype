using UnityEngine;
using System.Collections;

public class WeaponMelee : Weapon 
{
   
	// Use this for initialization
	void Awake () 
    {
        meleeCool = 0.35f;
        SetWeapon(20, 0.7f, true, 0, 0,20);
	}
	
	// Update is called once per frame
	public override void Update () 
    {
        base.Update();
	}
}
