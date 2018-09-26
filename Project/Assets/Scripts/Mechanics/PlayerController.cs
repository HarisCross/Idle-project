using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float Money = 1000;
    public Text text_Money;private string string_money;


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
        string_money = "Funds: " + Money.ToString() + ".";
    }


}
