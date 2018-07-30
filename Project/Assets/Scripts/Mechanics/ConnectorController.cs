using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorController : MonoBehaviour {

    public GameObject SpaceTaken;

    public List<GameObject> BoxesToExport = new List<GameObject>();
    public List<GameObject> BoxesToImport = new List<GameObject>();

    //  private List<GameObject> recieveList = new List<GameObject>();
    private List<GameObject> sendList = new List<GameObject>();

    //box values//
    [SerializeField]
    public float IncomeHeld = 0f;
    public float transferRate = 15f;

    //box values//

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
