using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerManager : MonoBehaviour 
{
    FollowerManager instance = null;
    List<Follower> FollowerList = new List<Follower>();
    List<Follower> Suckers = new List<Follower>();
    public Follower follower;
	// Use this for initialization
	void Awake () 
    {
	    if (instance == null)
        {
            instance = this;
        }
     }

    // Update is called once per frame
	void Update ()
    {
	
	}

   public void CreateFollowers(int amount, List<Vector3>positions)
    {
       for (int i=0;i<amount;i++)
       {
          Follower f = Instantiate(follower);
          f.gameObject.transform.position = positions[Random.Range(0, positions.Count)];
          FollowerList.Add(f);
       }
    }
    public void ChangeLists()
   {
       foreach (Follower f in FollowerList)
       {
           if (f.succumbed)
           {
               Suckers.Add(f);
               FollowerList.Remove(f);
           }
       }
   }
}
