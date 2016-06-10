using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillBars : MonoBehaviour 
{
    public Slider healthBar;
    public Image healthSlider;
    public Slider influenceBar;
    public Image influenceSlider;
    public static FillBars instance = null;
	// Use this for initialization
	void Start () 
    {
	    if (instance == null)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        FillBarsAtZero();
	}
    public void HealthDisplay(float amount)
    {
        healthBar.value = amount;
    }

    public void InfluenceDisplay (float amount)
    {
        influenceBar.value = amount;
    }
    void FillBarsAtZero()
    {
        if (healthBar.value <= 0)
        {
            Debug.Log(healthBar.value);
            healthSlider.enabled = false;
        }
        if (influenceBar.value <= 0)
        {
            influenceSlider.enabled = false; 
        }
    }
}
