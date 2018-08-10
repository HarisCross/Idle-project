using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorController : MonoBehaviour {

    public GameObject SpaceTaken;
    private GameObject closest;


    public List<GameObject> ConnectorSides;

    public List<GameObject> AdjSides;//sides next to this box
    public List<GameObject> AdjObjOnSides;

    public List<GameObject> BoxesToExport = new List<GameObject>();
    public List<GameObject> BoxesToImport = new List<GameObject>();

    public List<GameObject> ExportSides;//to store the items to send money too
    public List<GameObject> AcceptingSides;//sides which this can accept from

    //box values//
    [SerializeField]
    public float IncomeHeld = 0f;
    public float transferRate = 15f;
    public float MaxLimit = 350f;
    //box values//

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (AdjSides.Count == 0)
        {
            AdjSides.AddRange(SpaceTaken.GetComponent<BoardSpaceController>().PossibleSides);
            UpdateAbjObjOnSides();
        }


    }
    public void Timedpdate()
    {
      
    }
    
    public GameObject GetNearObjects(Transform pos, bool AddTile)
    {
        closest = null;

        foreach (GameObject side in ExportSides)
        {


            if (closest == null)
            {
                closest = side;
            }
            else
            {

                if (Vector3.Distance(closest.gameObject.transform.position, pos.transform.position) > Vector3.Distance(pos.position, side.transform.position))
                {
                    closest = side;
                }


            }

        }
        foreach (GameObject side in AcceptingSides)
        {


            if (closest == null)
            {
                closest = side;
            }
            else
            {

                if (Vector3.Distance(closest.gameObject.transform.position, pos.transform.position) > Vector3.Distance(pos.position, side.transform.position))
                {
                    closest = side;
                }


            }

        }

        UpdateConnObj();
        return closest;
    }
    public void UpdateConnObj()
    {
        BoxesToExport.Clear();
        BoxesToImport.Clear();

        foreach (GameObject side in ExportSides)
        {

            if (side.GetComponent<BoxSideController>().inpExpSide != null)
            {
                BoxesToExport.Add(side.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);
            }

        }


        foreach (GameObject side in AcceptingSides)
        {
            if (side.GetComponent<BoxSideController>().inpExpSide != null)
            {
                BoxesToImport.Add(side.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);
            }

        }

    }
    public void UpdateAbjObjOnSides()//called when change oocurs to map to update adj objects
    {

        foreach (GameObject side in AdjSides)
        {

            if (side.GetComponent<BoardSpaceController>().CurrentBox != null)
            {
                AdjObjOnSides.Add(side);
            }


        }



    }     
    private void MoneyTransferExport()
    {

        IncomeHeld = Mathf.Round(IncomeHeld);
        int count = BoxesToExport.Count;
        float moneySplit;

        foreach (GameObject box in BoxesToExport)
        {
            if (CanTransfer(box) == false)
            {
                //  Debug.Log("can trasnfer failed");
                return;
            }

            if (IncomeHeld > transferRate)
            {
                //if held is more than transfer rate
                // box.AddIncome(transferRate);


                moneySplit = Mathf.Round((transferRate / count));


                if (box.GetComponent<BoxController>() != null)
                {

                    box.GetComponent<BoxController>().IncomeHeld += moneySplit;


                }

                if (box.GetComponent<ConnectorController>() != null)
                {

                    box.GetComponent<ConnectorController>().IncomeHeld += moneySplit;


                }


                if (box.GetComponent<RecieverController>() != null)
                {
                    box.GetComponent<RecieverController>().IncomeHeld += moneySplit;


                }

                Debug.Log("TRansfered money");


                IncomeHeld -= transferRate;
            }
            else
            {
                //if held isnt more than rate
                //  box.AddIncome(IncomeHeld);

                moneySplit = Mathf.Round(IncomeHeld / count);


                if (box.GetComponent<BoxController>() != null)
                {

                    box.GetComponent<BoxController>().IncomeHeld += moneySplit;


                }

                if (box.GetComponent<ConnectorController>() != null)
                {

                    box.GetComponent<ConnectorController>().IncomeHeld += moneySplit;


                }


                if (box.GetComponent<RecieverController>() != null)
                {
                    box.GetComponent<RecieverController>().IncomeHeld += moneySplit;


                }

                Debug.Log("TRansfered money");

                IncomeHeld -= IncomeHeld;
            }

        }






    }

    private bool CanTransfer(GameObject box)
    {
        bool ret = false;
        Debug.Log(box.name);


        if (box.GetComponent<BoxController>() != null)
        {
            Debug.Log("transferred to box");

            foreach (GameObject pot in box.GetComponent<BoxController>().BoxesToImport)
            {
                if (pot.gameObject == this.gameObject)
                {
                    Debug.Log("can trasnfer: pass");
                    ret = true;

                }
                else
                {
                    Debug.Log("can trasnfer: fail: " + pot.gameObject + " : " + this.gameObject);
                    ret = false;
                }
            }
        }

        if (box.GetComponent<ConnectorController>() != null)
        {
            Debug.Log("transferred to connectror");

            foreach (GameObject pot in box.GetComponent<ConnectorController>().BoxesToImport)
            {
                if (pot.gameObject == this.gameObject)
                {
                    Debug.Log("can trasnfer: pass");
                    ret = true;

                }
                else
                {
                    Debug.Log("can trasnfer: fail: " + pot.gameObject + " : " + this.gameObject);
                    ret = false;
                }

            }



        }


        if (box.GetComponent<RecieverController>() != null)
        {
            Debug.Log("transferred to rec");


            foreach (GameObject pot in box.GetComponent<RecieverController>().BoxesToImport)
            {
                Debug.Log(pot.name + this.name);
                if (pot.gameObject == this.gameObject)
                {
                    Debug.Log("can trasnfer: pass");
                    ret = true;

                }
                else
                {
                    Debug.Log("can trasnfer: fail: " + pot.gameObject + " : " + this.gameObject);
                    ret = false;
                }

            }


        }




        return ret;
    }




}
