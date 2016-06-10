using UnityEngine;
using System.Collections;

public class WeaponFists : Weapon
{

	// Use this for initialization
	void Start () 
    {
        meleeCool = 0.2f;
        SetWeapon(1, 0.5f, true, 0, 0,10);
	}
	
	// Update is called once per frame
	public override void Update () 
    {
        base.Update();
	}
}
