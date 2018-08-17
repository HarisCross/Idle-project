using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnButtonController : MonoBehaviour {

    public bool Active = false;
    public bool ButtonsActive = false;
    public List<GameObject> ConnButtons = new List<GameObject>();

    public GameObject ConnTarget;
    public List<GameObject> ConnSides = new List<GameObject>();


    //conn values//
    private float SideCost = 50f;


    //conn values//

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (ConnTarget != null)
        {
            //add its sides to list
            if (ConnSides.Count == 0) { 
                ConnSides.AddRange(ConnTarget.GetComponent<ConnectorController>().ConnectorSides);

            }
            //      ActivateButtons();
         
        }
        else
        {
            //clear the liust

            ConnSides.Clear();
          //      DeactivateButtons();
        }
        if (Active == false)
        {
            ConnTarget = null;
            ConnSides.Clear();
        }

        if (Active) MoveUI();
    }
    private void MoveUI()
    {
        //for each side get button and have hover over side pos

        int count = 0;

        foreach(GameObject side in ConnSides)
        {

            Vector3 sidePos = Camera.main.WorldToScreenPoint(side.transform.position);

            ConnButtons[count].transform.position = sidePos;



            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded == false)
            {
                ConnectorSide CSide = side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>();
                // button.GetComponent<Button>().onClick.AddListener(CSide.ModifySide);

                ConnButtons[count].gameObject.GetComponent<Button>().onClick.AddListener(CSide.ModifySide);


                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded = true;
            }
            count++;
        }


    }
    public void testFunc()
    {
        Debug.Log(this.name);
    }
    public void ActivateButtons()
    {
        // Debug.Log("activate buttons");
       // int count = 0;
        foreach (GameObject butt in ConnButtons)
        {
            //Debug.Log("doing: "+butt.name);
            StartCoroutine(FillButton(butt,true));


        }

        int count = 0;

        foreach (GameObject side in ConnSides)
        {

            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded == false)
            {
                ConnectorSide CSide = side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>();
                ConnButtons[count].gameObject.GetComponent<Button>().onClick.AddListener(CSide.ModifySide);


                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded = true;
            }
            count++;
        }
    }
    public void DeactivateButtons()
    {
        //Debug.Log("deactivate buttons");
        foreach(GameObject butt in ConnButtons)
        {
           // Debug.Log("doing: "+butt.name);
            StartCoroutine(FillButton(butt,false));
        }
        int count = 0;

        foreach (GameObject side in ConnSides)
        {

            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded == true)
            {
                //   ConnectorSide CSide = side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>();
                //   ConnButtons[count].gameObject.GetComponent<Button>().onClick.AddListener(CSide.ModifySide);

                ConnButtons[count].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded = false;
            }
            count++;
        }

    }

    private IEnumerator FillButton(GameObject button,bool var)
    {
        float timer = 1f;
        if (var)
        {
            do
            {

               // button.GetComponent<Image>().fillAmount += 0.01f;


                button.GetComponent<Image>().fillAmount += timer * Time.deltaTime;
                yield return new WaitForSecondsRealtime(0.01f);
              //  Debug.Log("filling");

            } while (button.GetComponent<Image>().fillAmount <0.95f);



           // Debug.Log("stopped filling");
            button.GetComponent<Image>().fillAmount = 1f;
            button.GetComponent<Button>().interactable = true;
            button.transform.GetChild(0).transform.gameObject.SetActive(true);




        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            button.transform.GetChild(0).transform.gameObject.SetActive(false);
            do
            {

                button.GetComponent<Image>().fillAmount -= timer * Time.deltaTime;
                yield return new WaitForSecondsRealtime(0.01f);

            } while (button.GetComponent<Image>().fillAmount >0);





        }
        yield return null;
    }

    public void Modify()
    {
        //allow purchase, upgrade to lv 3




    }

}
