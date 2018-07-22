﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSideController : MonoBehaviour {

    public BoxController MainBox;
    public float rotateAmount;
    public bool boxSidePresent = false;


    //box side values//
    // public enum SideType { Input, Output, BonusModifier }
    public string side = "";
    public float IncomeHeld = 0;
    public float IncomeRate = 0f;
    public float transferRate = 2.5f;

    //box side values//

    public int SideNumber;

    // Use this for initialization
    void Start () {
		
	}

    public void Timedpdate()
    {
        IncomeHeld += IncomeRate;
       // Debug.Log("box side timed");

        //add to main box
        //take from incomeheld
        if(IncomeHeld > transferRate)
        {
            //if held is more than transfer rate

            MainBox.AddIncome(transferRate);
            IncomeHeld -= transferRate;

        }
        else
        {
            //if held isnt more than rate

            MainBox.AddIncome(IncomeHeld);
            IncomeHeld -= IncomeHeld;
        }
    }
    // Update is called once per frame
    void Update () {

        if (MainBox == null)
        {

            Collider[] col = Physics.OverlapSphere(this.transform.position, 1f);
            int i = 0;
            while (i < col.Length)
            {
                if (col[i].transform.gameObject.tag == "MainBox")
                {
                    MainBox = col[i].transform.gameObject.GetComponent<BoxController>();
                    MainBox.boxSides.Add(this.transform.gameObject);

                }
            }

        }

    }
}
