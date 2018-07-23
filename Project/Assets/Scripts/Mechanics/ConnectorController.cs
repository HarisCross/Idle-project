using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorController : MonoBehaviour {

    public GameObject SpaceTaken;


  //  private List<GameObject> recieveList = new List<GameObject>();
    private List<GameObject> sendList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Timedpdate()
    {
      
    }
    public void UpdateAbjObjOnSides()//called when change oocurs to map to update adj objects
    {
        //AdjObjOnSides.Clear();
        //foreach (GameObject side in AdjSides)
        //{

        //    if (side.GetComponent<BoardSpaceController>().CurrentBox != null)
        //    {
        //        AdjObjOnSides.Add(side);
        //    }


        //}



    }
}
