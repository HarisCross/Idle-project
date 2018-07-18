using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbuttonController : MonoBehaviour {


    private UIInteraction UI;

    public int switchButton=0;
	// Use this for initialization
	void Start () {

        UI = GameObject.Find("UI").GetComponent<UIInteraction>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if(UI.menuDisplayed == true)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }

	}

}
