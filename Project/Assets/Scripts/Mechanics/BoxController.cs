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

    public List<GameObject> SurroundingTiles;

    public List<GameObject> BoxesToExport = new List<GameObject>();
    public List<GameObject> BoxesToImport = new List<GameObject>();

    private bool BoxFocused = false;
    GameObject closest;

    //box values//
   
    public float IncomeHeld = 0f;
    public float transferRate = 15f;
    public float MaxLimit = 1500f;
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
            MoneyTransferExport();////add check to see if the box potentialy being added too has an import plug


    }
    private bool CanTransfer(GameObject box)
    {
        bool ret = false;
        Debug.Log(box.name);


        if (box.GetComponent<BoxController>() != null)
        {
        //    Debug.Log("transferred to box");

            foreach (GameObject pot in box.GetComponent<BoxController>().BoxesToImport)
            {
                if (pot.gameObject == this.gameObject)
                {
                  //  Debug.Log("can trasnfer: pass");
                    ret = true;

                }
                else
                {
                    Debug.Log("can trasnfer: fail: " + pot.gameObject + " : " + this.gameObject);
                    ret = false;
                }
            }
        }

        if(box.GetComponent<ConnectorController>() != null)
        {
          //  Debug.Log("transferred to connectror");

            foreach (GameObject pot in box.GetComponent<ConnectorController>().BoxesToImport)
            {
                if (pot.gameObject == this.gameObject)
                {
                //    Debug.Log("can trasnfer: pass");
                    ret = true;

                }
                else
                {
                    Debug.Log("can trasnfer: fail: " + pot.gameObject + " : " + this.gameObject);
                    ret = false;
                }

            }



        }


        if(box.GetComponent<RecieverController>() != null)
        {
         //   Debug.Log("transferred to rec");


            foreach (GameObject pot in box.GetComponent<RecieverController>().BoxesToImport)
            {
                Debug.Log(pot.name + this.name);
                if (pot.gameObject == this.gameObject)
                {
                 //   Debug.Log("can trasnfer: pass");
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
    private void MoneyTransferExport()
    {
        
        IncomeHeld = Mathf.Round(IncomeHeld);
        int count = BoxesToExport.Count;
        float moneySplit;

        foreach (GameObject box in BoxesToExport)
        {
            if(CanTransfer(box) == false)
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




              //  Debug.Log("TRansfered money");


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







//
               // Debug.Log("TRansfered money");


                IncomeHeld -= IncomeHeld;
            }

        }
   





    }
    public void AddIncome(float amount)
    {
     //   Debug.Log("incomed added to: " + this.name);
        IncomeHeld += amount;
    }
    // Use this for initialization
    void Start () {


        UpdateConnObj();


    }
	
	// Update is called once per frame
	void Update () {
		
        if(AdjSides.Count == 0)
        {
            AdjSides.AddRange(SpaceTaken.GetComponent<BoardSpaceController>().PossibleSides);
            UpdateAbjObjOnSides();
        }

        if (IncomeHeld > MaxLimit)
        {
            IncomeHeld = MaxLimit;
        }
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
        foreach (GameObject connector in ImpExpSides)
        {

            //if (connector.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox != null)
            //{

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
                UpdateConnObj();
            }
       // }


    }
    public void RemoveNearTile(GameObject tile)//remove the inp/exp tile from the list
    {
       GameObject tileToRemove= GetNearObjects(tile.transform,false);

        SurroundingTiles.Remove(tileToRemove);


    }
    public GameObject GetNearObjects(Transform pos,bool AddTile)
    {
        closest = null;
       


        foreach (GameObject side in AdjSides)
        {


            if(closest == null)
            {
                closest = side;
            }
            else
            {
                //float dist = Vector3.Distance(pos.position, side.transform.position);
              //  Debug.Log("dist: " + dist);

                if (Vector3.Distance(closest.gameObject.transform.position, pos.transform.position) > Vector3.Distance(pos.position, side.transform.position))
                {
                    closest = side;
                }


            }

        }

        //  Debug.Log("closest is: " + closest.name);
        if (AddTile)
        {
            SurroundingTiles.Add(closest.gameObject);

        }
        UpdateConnObj();
        return closest;
    }

    public void UpdateConnObj()
    {
        BoxesToExport.Clear();
        BoxesToImport.Clear();


        //use bsc - connector, 2 or 1, tile. update whenever it changes
      //  Debug.Log("export count: " + ExportSides.Count);
        foreach(GameObject side in ExportSides)
        {

            if (side.GetComponent<BoxSideController>().inpExpSide != null)
            {
                //  Debug.Log("adding: " + side.GetComponent<BoxSideController>().inpExpSide);
                if (side.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox != null)
                {
                    BoxesToExport.Add(side.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);
                }
                else
                {
                    Debug.Log("space doesnt have a box");
                }
            }

        }


        foreach (GameObject side in AcceptingSides)
        {
            // Debug.Log("adding: " + side.GetComponent<BoxSideController>().inpExpSide);
            if (side.GetComponent<BoxSideController>().inpExpSide != null) {
                if (side.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox != null)
                {
                    BoxesToImport.Add(side.GetComponent<BoxSideController>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);
                }
                else
                {
                    Debug.Log("space doesnt have a box");
                }
            }

        }






    }






}
