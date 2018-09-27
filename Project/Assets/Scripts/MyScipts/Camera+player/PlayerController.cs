using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public double Money = 1000;
    public Text text_Money;private string string_money;


	// Use this for initialization
	void Start () {
        string_money = "Funds:\n " + Money.ToString() + ".";
    }
	
	// Update is called once per frame
	void Update () {
        text_Money.text = string_money;
	}
    public void AddIncome(float amount)
    {
        //   Debug.Log("incomed added to: " + this.name);
        double INTAmount = Mathf.RoundToInt(amount);
        Money += INTAmount;
        string_money = "Funds:\n " + Money.ToString() + ".";
    }


}
