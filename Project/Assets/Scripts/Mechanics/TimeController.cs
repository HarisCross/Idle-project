using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeController : MonoBehaviour {

    public float IncTime=2;
    private BoardController boardController;
    private RecieverController RecieverController;

    public List<GameObject> BoxList = new List<GameObject>();
    public List<GameObject> SideList = new List<GameObject>();
    public List<GameObject> ConnectorList = new List<GameObject>();

	// Use this for initialization
	void Start () {

        InvokeRepeating("TimeIncrementer", IncTime, IncTime);
        boardController = GameObject.Find("BoardGrid").GetComponent<BoardController>();
        RecieverController = GameObject.Find("BoxReciever").GetComponent<RecieverController>();
	}
	
	// Update is called once per frame
	void Update () {


     

	}
    public void UpdateAddList(GameObject newObject)
    {
        //add item to list of stuff to update
        //updateList.Add(newObject);

        if(newObject.GetComponent<BoxController>() != null)
        {
           // Debug.Log("added box");
            BoxList.Add(newObject);
        }
        if (newObject.GetComponent<BoxSideController>() != null)
        {
          //  Debug.Log("added side");
            SideList.Add(newObject);
        }
        if (newObject.GetComponent<ConnectorController>() != null)
        {
           // Debug.Log("added connectoe");
            ConnectorList.Add(newObject);
        }
        UpdateBoxExports();

    }
    public void UpdateRemoveList(GameObject RemoveObject)
    {
        //remove item from list of stuff to update
        //updateList.Remove(RemoveObject);
       // RemoveObject.GetComponent<BoxController>().SpaceTaken.GetComponent<BoardSpaceController>().CurrentBox = null;
        if (RemoveObject.GetComponent<BoxController>() != null)
        {
            RemoveObject.GetComponent<BoxController>().SpaceTaken.GetComponent<BoardSpaceController>().CurrentBox = null;
            BoxList.Remove(RemoveObject);
        }
        if (RemoveObject.GetComponent<BoxSideController>() != null)
        {
            SideList.Remove(RemoveObject);
        }
        if (RemoveObject.GetComponent<ConnectorController>() != null)
        {
            RemoveObject.GetComponent<ConnectorController>().SpaceTaken.GetComponent<BoardSpaceController>().CurrentBox = null;
            ConnectorList.Remove(RemoveObject);
        }
        UpdateBoxExports();
    }
    private void UpdateBoxExports()
    {
        foreach(GameObject side in BoxList)
        {
            side.GetComponent<BoxController>().UpdateAbjObjOnSides();
            side.GetComponent<BoxController>().UpdateConnObj();

        }
        foreach(GameObject side in ConnectorList)
        {

            side.GetComponent<ConnectorController>().UpdateAbjObjOnSides();
        }
    }
    void TimeIncrementer()
    {

        boardController.UpdatePossBOSList();


        //have if statements and switch to detemine what type the item is  and then get its component and call update function
        //update the reciever

        RecieverController.Timedpdate();
        if (BoxList.Count != 0)
        {

            foreach (GameObject box in BoxList)
            {

                box.GetComponent<BoxController>().Timedpdate();

            }
        }

        if(SideList.Count != 0){


            foreach (GameObject side in SideList)
            {
                side.GetComponent<BoxSideController>().Timedpdate();

            }
        }

        if (ConnectorList.Count != 0)
        {
            foreach (GameObject connector in ConnectorList)
            {

                connector.GetComponent<ConnectorController>().Timedpdate();

            }
        }
    }

   
}
