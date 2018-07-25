using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

   public  List<GameObject> boxSides = new List<GameObject>();
    public GameObject SpaceTaken;


    public List<GameObject> AdjSides;//sides next to this box
    public List<GameObject> AdjObjOnSides;

   // public List<GameObject> ConnectorSides;
    public List<GameObject> ExportSides;//to store the items to send money too
    public List<GameObject> AcceptingSides;//sides which this can accept from

    public List<GameObject> ImpExpSides;//sides with a connector



    private bool BoxFocused = false;

    //box values//
    [SerializeField]
    private float IncomeHeld = 0f;
    public float transferRate = 15f;

    //box values//

    private void Awake()
    {


        if (boxSides.Count == 0)
        {

            Collider[] col = Physics.OverlapSphere(this.transform.position, 9f);
            //Debug.Log("col" + col.Length);
            int i = 0;
            while (i < col.Length)
            {
                if (col[i].transform.gameObject.tag == "BoxSide")
                {
                  //  Debug.Log(Vector3.Distance(this.gameObject.transform.position, col[i].gameObject.transform.position));
                    boxSides.Add(col[i].gameObject);

                }
                i++;
            }

        }



    }
    public void Timedpdate()
    {

    }
    public void AddIncome(float amount)
    {
     //   Debug.Log("incomed added to: " + this.name);
        IncomeHeld += amount;
    }
    // Use this for initialization
    void Start () {

      



    }
	
	// Update is called once per frame
	void Update () {
		
        if(AdjSides.Count == 0)
        {
            AdjSides.AddRange(SpaceTaken.GetComponent<BoardSpaceController>().PossibleSides);
            UpdateAbjObjOnSides();
        }

	}
    public  void UpdateLists()
    {




    }
    public void UpdateAbjObjOnSides()//called when change oocurs to map to update adj objects
    {

        foreach(GameObject side in AdjSides)
        {

            if (side.GetComponent<BoardSpaceController>().CurrentBox != null)
            {
                AdjObjOnSides.Add(side);
            }


        }



    }
    public void UpdateInpExpLists()
    {
        ExportSides.Clear();
        AcceptingSides.Clear();
        foreach(GameObject connector in ImpExpSides)
        {
            
            switch (connector.GetComponent<BoxSideController>().connectorStatus)
            {

                case 0:
                    //not connector
                    Debug.Log("somehow connector hasnt been assigned as a connector");
                    break;
                case 1:
                    //exp side
                    ExportSides.Add(connector);

                    break;
                case 2:
                    //inp side
                    AcceptingSides.Add(connector);

                    break;
                default:

                    break;

            }
        }



    }
}
