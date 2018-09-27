using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    // public int[,] boardGrid;
    //GameObject tempGO;

    public bool DisplayingFreeBoxSpaces = false;
    public List<GameObject> PossibleBuildingOpenSpots; // do at soem point
    public GameObject[] BoardSpaces ;

    public UIInteraction UIInteraction;

    public bool DisplayAvail = false;

    // Use this for initialization
    void Start()
    {

        //boardGrid = new int[5, 5];
        UpdateBoardSpaceControllerSidesList();

        BoardSpaces = GameObject.FindGameObjectsWithTag("BoardSpace");
        UIInteraction = GameObject.Find("UI").GetComponent<UIInteraction>();



    }
    // Update is called once per frame
    void Update()
    {

    }
    public void DisplayFreeBoxSpacesBox()
    {
       // Debug.Log("Check");
        DisplayAvail = !DisplayAvail;


        UIInteraction.BoxOrConn = true;

        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            if (space.GetComponent<BoardSpaceController>().TakeByS == false)
            {
                if (space.GetComponent<BoardSpaceController>().TakenByB == false)
                {
                  //  Debug.Log("Value Changed");
                    space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
                }

            }

        }

    }
    public void DisplayFreeBoxSpacesConn()
    {
        // Debug.Log("Check");
        DisplayAvail = !DisplayAvail;

        UIInteraction.BoxOrConn = false;

        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            if (space.GetComponent<BoardSpaceController>().TakeByS == false)
            {
                if (space.GetComponent<BoardSpaceController>().TakenByB == false)
                {
                    //  Debug.Log("Value Changed");
                    space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
                }

            }

        }

    }
    public void UpdatePossBOSList()//function called to update the board- adds avail spots to list if no item currently there 
    {
        PossibleBuildingOpenSpots.Clear();
        foreach(GameObject spot in BoardSpaces)
        {
            if(spot.GetComponent<BoardSpaceController>().TakeByS == false)
            {
                if(spot.GetComponent<BoardSpaceController>().TakenByB == false)
                {
                    if (spot.GetComponent<BorderSpaceTile>())
                    {

                        PossibleBuildingOpenSpots.Add(spot);
                    }
                }

            }
        }

    }
    void UpdateBoardSpaceControllerSidesList()//updates the avilable spaces on the board to show possible places for item
    {

        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j < 6; j++)
            {

                if (i + 1 < 6)
                {
                    string currentSpace = i.ToString() + j.ToString();
                    string NextSpace = (i + 1).ToString() + j.ToString();
                    GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                }
                if (i - 1 > 0)
                {
                    string currentSpace = i.ToString() + j.ToString();
                    string NextSpace = (i - 1).ToString() + j.ToString();
                    GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                }

                if (j + 1 < 6)
                {
                    string currentSpace = i.ToString() + j.ToString();
                    string NextSpace = i.ToString() + (j + 1).ToString();
                    GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                }
                if (j - 1 > 0)
                {
                    string currentSpace = i.ToString() + j.ToString();
                    string NextSpace = i.ToString() + (j - 1).ToString();
                    GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                }

            }


        }
    }

} 
