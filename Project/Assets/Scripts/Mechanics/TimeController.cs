using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeController : MonoBehaviour {

    public int timer=0;

	// Use this for initialization
	void Start () {

        InvokeRepeating("TimeIncrementer",1f,1f);
        
	}
	
	// Update is called once per frame
	void Update () {


     

	}
    void TimeIncrementer()
    {

        GameObject.Find("BoardGrid").GetComponent<BoardController>().UpdatePossBOSList();

        timer++;
        //Debug.Log(timer);
    }
}
