using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Money = 1000;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void AddIncome(float amount)
    {
        //   Debug.Log("incomed added to: " + this.name);
        Money += amount;
    }
}
