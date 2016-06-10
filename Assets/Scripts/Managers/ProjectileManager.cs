using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileManager : MonoBehaviour 
{
    public static ProjectileManager instance = null;
  //  public Projectile projectile;
    List<Projectile> projectilePool = new List<Projectile>();
	// Use this for initialization
	void Awake() 
    {
	    if (instance == null)
        {
            instance = this;
        }
	}
	
    public Projectile PoolingProjectiles(Projectile proj)
    {
        for (int i=0;i<projectilePool.Count;i++)
        {
            if (!projectilePool[i].isActiveAndEnabled)
            {
                projectilePool[i].enabled = true;
                return projectilePool[i];
            }
        }

        Projectile p = Instantiate(proj);
        projectilePool.Add(p);
        return p;

    }
}
