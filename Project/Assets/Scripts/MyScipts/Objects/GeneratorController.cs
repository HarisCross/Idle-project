using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    private GameObject closest;
    public GameObject SpaceTaken;
    public GameObject ConnSide;

    public List<GameObject> AcceptingSides;//sides which this can accept from

    public List<GameObject> AdjSides;//sides next to this box
    public List<GameObject> AdjObjOnSides;
    public List<GameObject> BoxesToImport = new List<GameObject>();
    public List<GameObject> SurroundingTiles;

    public List<GameObject> ImpExpSides;//sides with a connector

    //box values//
    [SerializeField]
    public bool AutoFeedConversion = true;
    public bool AutoFeedTransfer = false;

    public float IncomeHeld = 0f;
    public float FeedHeld = 0f;
    public float transferRate = 10f;
    public float FeedConversionRate = 20f;
    public float FeedConversionRateModifier = 0.5f;
    public float MaxLimit = 350f;
    private float MaxLimitFeed = 250;
    //box values//

    // Use this for initialization
    private void Start()
    {
        if (AdjSides.Count == 0)
        {
            AdjSides.AddRange(SpaceTaken.GetComponent<BoardSpaceController>().PossibleSides);
            UpdateAbjObjOnSides();
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Timedpdate() //called by time controlelr to make stuff do things
    {
        FundsConversion();
    }

    private void FundsConversion()//turn money into feed if limits allow
    {
        //check current feed agasinst maximumm and see if you can add the max trasnfer amount or if need to do lower,
        //if so then take current from maximum to find out how much to convert.

        if (!AutoFeedConversion) return;

        float incomeAvailToConvert;//number to hold how much income to convert

        if (IncomeHeld > FeedConversionRate)
        {
            //has enough money

            incomeAvailToConvert = FeedConversionRate;
            IncomeHeld -= incomeAvailToConvert;
        }
        else
        {
            //doesnt have enough money so uses what it has

            incomeAvailToConvert = IncomeHeld;
            IncomeHeld = 0;
        }

        int tempConvertAmount = Mathf.RoundToInt((incomeAvailToConvert * FeedConversionRateModifier));//int to hold the convert income to feed amount- will be less as default is half

        FeedHeld += tempConvertAmount;//add to feed

        if (FeedHeld > MaxLimitFeed) FeedHeld = MaxLimitFeed;

        FeedHeld = Mathf.RoundToInt(FeedHeld);
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

    public void RemoveNearTile(GameObject tile)//remove the inp/exp tile from the list
    {
        GameObject tileToRemove = GetNearObjects(tile.transform, false);

        SurroundingTiles.Remove(tileToRemove);
    }

    public GameObject GetNearObjects(Transform pos, bool AddTile)
    {
        closest = null;

        foreach (GameObject side in AdjSides)
        {
            if (closest == null)
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
            if (closest != null)
            {
                SurroundingTiles.Add(closest.gameObject);
            }
            else
            {
                print("closest is empty- should of be given a value");
            }
        }
        UpdateConnObj();
        return closest;
    }

    public void UpdateConnObj()
    {
        //BoxesToExport.Clear();
        BoxesToImport.Clear();

        // Debug.Log("update conn obj " + this.name);

        //use bsc - connector, 2 or 1, tile. update whenever it changes
        //  Debug.Log("export count: " + ExportSides.Count);
        //foreach (GameObject side in ExportSides)
        //{
        //    if (side.transform.parent.GetComponent<ConnectorSide>().inpExpSide != null)
        //    {
        //        //  Debug.Log("adding: " + side.GetComponent<BoxSideController>().inpExpSide);
        //        if (side.transform.parent.GetComponent<ConnectorSide>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox != null)
        //        {
        //            BoxesToExport.Add(side.transform.parent.GetComponent<ConnectorSide>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);
        //        }
        //        else
        //        {
        //            // Debug.Log("space doesnt have a box");
        //        }
        //    }

        //}

        foreach (GameObject side in AcceptingSides)
        {
            // Debug.Log("adding: " + side.GetComponent<BoxSideController>().inpExpSide);
            if (side.GetComponent<GeneratorSide>().inpExpSide != null)
            {
                if (side.GetComponent<GeneratorSide>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox != null)
                {
                    //print(side.name);
                    //print(side.transform.parent.GetComponent<ConnectorSide>().inpExpSide.gameObject.name);
                    //print(side.GetComponent<ConnectorSide>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);

                    BoxesToImport.Add(side.GetComponent<GeneratorSide>().inpExpSide.GetComponent<BoardSpaceController>().CurrentBox);
                }
                else
                {
                    //Debug.Log("space doesnt have a box");
                }
            }
        }
    }

    public void UpdateInpExpLists()
    {
        //ExportSides.Clear();
        AcceptingSides.Clear();
        foreach (GameObject side in ImpExpSides)
        {
            switch (side.GetComponent<GeneratorSide>().connectorStatus)
            {
                case 0:
                    //not connector
                    Debug.Log("somehow connector hasnt been assigned as a connector");
                    break;

                case 1:
                    //exp side
                    // ExportSides.Add(side);

                    break;

                case 2:
                    //inp side
                    AcceptingSides.Add(side);

                    break;

                default:

                    break;
            }
            UpdateConnObj();
        }
        // }
    }
}