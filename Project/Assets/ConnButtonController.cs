using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnButtonController : MonoBehaviour {

    public bool Active = false;

    public List<GameObject> ConnButtons = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TestClick()
    {
        Debug.Log("test button");
    }

    //FILL AMOUNT,CHILD - TEXT, BUTTON-INTERACTABLE
    public void TESTActivate()
    {
        ActivateButtons();

    }
    public void TESTDeactivate()
    {


    }
    public void ActivateButtons()
    {

        foreach(GameObject butt in ConnButtons)
        {
            Debug.Log("doing: "+butt.name);

            butt.GetComponent<Button>().interactable = true;
            butt.transform.GetChild(0).transform.gameObject.SetActive(true);

            StartCoroutine(FillButton(butt));



        }


    }

    private IEnumerator FillButton(GameObject button)
    {
        float timer = 2f;

        do
        {

            //button.GetComponent<Image>().fillAmount += 0.01f;


            button.GetComponent<Image>().fillAmount += timer * Time.deltaTime;
            yield return new WaitForSecondsRealtime(0.01f);

        } while (button.GetComponent<Image>().fillAmount < 1);

        yield return null;
    }



}
