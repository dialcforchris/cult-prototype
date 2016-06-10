using UnityEngine;
using System.Collections;

public class BloodManager : MonoBehaviour 
{
    public static BloodManager instance = null;
    public ParticleSystem blood;

	// Use this for initialization
	void Start () 
    {
        if (instance == null)
        {
            instance = this;
        }
	}
	
    public void PlayBlood(Vector2 pos, Vector2 rot)
    {
        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        blood.transform.position = pos;
        blood.transform.rotation = Quaternion.LookRotation(pos + -rot);
        blood.Play();
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
}
