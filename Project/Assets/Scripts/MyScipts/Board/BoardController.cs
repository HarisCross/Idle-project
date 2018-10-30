using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    // public int[,] boardGrid;
    //GameObject tempGO;
    public GameObject[] BoardSides;

    public bool DisplayingFreeBoxSpaces = false;
    public List<GameObject> PossibleBuildingOpenSpots; // do at soem point
    public GameObject[] BoardSpaces;
    public List<GameObject> BoardBorderSpaces;

    //private UIInteraction UIInt;
    public UIInteraction UIInt;

    //  private GameObject currentSpace;                    GameObject currSpace;
    public bool DisplayAvail = false; private GameObject currSpace, nexSpace;

    private int displayActive = 0; // 0 for none, 1 for box, 2 for conn, 3 for purchase

    private void Awake()
    {
        UpdateBoardSpaceControllerSidesList();
    }

    // Use this for initialization
    private void Start()
    {
        //boardGrid = new int[5, 5];

        BoardSpaces = GameObject.FindGameObjectsWithTag("BoardSpace");
        UIInt = GameObject.Find("UI").GetComponent<UIInteraction>();

        foreach (GameObject space in BoardSpaces)
        {
            if (space.GetComponent<BorderSpaceTile>()) BoardBorderSpaces.Add(space);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //public void CloseActiveDisplay( )
    //{
    //    Debug.Log("should close display");
    //    switch (displayActive)
    //    {
    //        case 1:
    //            DisplayFreeBoxSpacesBox();
    //            displayActive = 0;
    //            break;
    //        case 2:
    //            DisplayFreeBoxSpacesConn();
    //            displayActive = 0;
    //            break;
    //        case 3:
    //            DisplayBoardTiles();
    //            displayActive = 0;
    //            break;
    //        case 0:break;

    //    }
    //}
    public void CloseDisplay()
    {
        DisplayAvail = false;
        UIInt.PurchasingTiles = false;

      //  print("closing display");
        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //if (space.GetComponent<BoardSpaceController>().TakeByS == false)
            //{
            if (space.GetComponent<BoardSpaceController>().TakenByB == false)
            {
                //  Debug.Log("Value Changed");
                space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
                space.GetComponent<BoardSpaceController>().ToggleCloseText();
            }

            //}
        }
    }

    public void DisplayFreeBoxSpacesBox()
    {
        //  Debug.Log("dispaly bboxs on tiles");
        if (DisplayAvail) { CloseDisplay(); return; }
        DisplayAvail = !DisplayAvail;

        //UIInt.BoxOrConn = true;
        UIInt.BoxConnGen = 1;

        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //if (space.GetComponent<BoardSpaceController>().TakeByS == false)
            //{
            if (space.GetComponent<BoardSpaceController>().TakenByB == false)
            {
                //  Debug.Log("Value Changed");
                space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
                space.GetComponent<BoardSpaceController>().ToggleCloseText();
            }

            //}
        }
    }

    public void DisplayFreeBoxSpacesGen()
    {
        //  Debug.Log("dispaly bboxs on tiles");
        if (DisplayAvail) { CloseDisplay(); return; }
        DisplayAvail = !DisplayAvail;

        //UIInt.BoxOrConn = true;
        UIInt.BoxConnGen = 3;

        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //if (space.GetComponent<BoardSpaceController>().TakeByS == false)
            //{
            if (space.GetComponent<BoardSpaceController>().TakenByB == false)
            {
                //  Debug.Log("Value Changed");
                space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
                space.GetComponent<BoardSpaceController>().ToggleCloseText();
            }

            //}
        }
    }

    public void DisplayBoardTiles()
    {
        //  Debug.Log("display board tiles for purchase");
        DisplayAvail = !DisplayAvail;
        //  if (displayActive == 0) displayActive = 3;

        // UIInteraction.BoxOrConn = true;

        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
            space.GetComponent<BoardSpaceController>().TogglePriceDisplay(true);
        }
    }

    public void DisplayFreeBoxSpacesConn()
    {
        // Debug.Log("display conns on tiles for purchase");
        DisplayAvail = !DisplayAvail;
        //   if (displayActive == 0) displayActive = 2;

        // UIInt.BoxOrConn = false;
        UIInt.BoxConnGen = 2;

        foreach (GameObject space in BoardSpaces)
        {
            space.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //if (space.GetComponent<BoardSpaceController>().TakeByS == false)
            //{
            if (space.GetComponent<BoardSpaceController>().TakenByB == false)
            {
                //  Debug.Log("Value Changed");
                space.gameObject.transform.GetChild(0).gameObject.SetActive(DisplayAvail);
                space.GetComponent<BoardSpaceController>().ToggleCloseText();
            }

            //}
        }
    }

    public void UpdatePossBOSList()//function called to update the board- adds avail spots to list if no item currently there
    {
        PossibleBuildingOpenSpots.Clear();
        foreach (GameObject spot in BoardSpaces)
        {
            //if(spot.GetComponent<BoardSpaceController>().TakeByS == false)
            //{
            if (spot.GetComponent<BoardSpaceController>().TakenByB == false)
            {
                if (spot.GetComponent<BorderSpaceTile>())
                {
                    PossibleBuildingOpenSpots.Add(spot);
                }
            }

            ////}
        }
    }

    public void UpdateBoardSpaceControllerSidesList()//updates the avilable spaces on the board to show possible places for item
    {
        int rows = 8;
        string currentSpace;
        string NextSpace;
        foreach (GameObject side in BoardSides)
        {
            string sideNum = side.GetComponent<BoardBoxSide>().boardBoxSide.ToString();
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < rows; j++)
                {
                    currentSpace = sideNum + i.ToString() + j.ToString();
                    NextSpace = sideNum + (i + 1).ToString() + j.ToString();
                    currSpace = null;
                    nexSpace = null;
                    nexSpace = GameObject.Find(NextSpace);
                    currSpace = GameObject.Find(currentSpace);
                    if (currSpace != null)
                    {
                        if (currSpace.GetComponent<BorderSpaceTile>())
                        {
                            // print("got a border");triggers on each border tile

                            //Collider[] col = Physics.OverlapSphere(currSpace.transform.position, 2f);

                            //foreach (Collider colliderHit in col)
                            //{
                            //    if (colliderHit.transform.GetComponent<BoardSpaceController>())
                            //    {
                            //    }

                            //}
                        }
                        else
                        {
                            if (i + 1 < rows)
                            {
                                currentSpace = sideNum + i.ToString() + j.ToString();
                                NextSpace = sideNum + (i + 1).ToString() + j.ToString();
                                //if (NextSpace == "11" || NextSpace == "71" || NextSpace == "77" || NextSpace == "17") print("got it");
                                //print(NextSpace);
                                try
                                {
                                    if (currSpace.GetComponent<BoardSpaceController>())
                                    {
                                        currSpace.GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                                    }
                                }
                                catch { print("board controller broke - adding the sides to the board tiles: " + currentSpace + " cant find " + NextSpace); }
                            }
                            if (i - 1 > 0)
                            {
                                currentSpace = sideNum + i.ToString() + j.ToString();
                                NextSpace = sideNum + (i - 1).ToString() + j.ToString();
                                // if (NextSpace == "11" || NextSpace == "71" || NextSpace == "77" || NextSpace == "17") break;
                                try
                                {
                                    if (currSpace.GetComponent<BoardSpaceController>())
                                    {
                                        currSpace.GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                                    }
                                }
                                catch { print("board controller broke - adding the sides to the board tiles: " + currentSpace + " cant find " + NextSpace); }
                                //   GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                            }

                            if (j + 1 < rows)
                            {
                                currentSpace = sideNum + i.ToString() + j.ToString();
                                NextSpace = sideNum + i.ToString() + (j + 1).ToString();
                                // if (NextSpace == "11" || NextSpace == "71" || NextSpace == "77" || NextSpace == "17") break;
                                //GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                                try
                                {
                                    if (currSpace.GetComponent<BoardSpaceController>())
                                    {
                                        currSpace.GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                                    }
                                }
                                catch { print("board controller broke - adding the sides to the board tiles: " + currentSpace + " cant find " + NextSpace); }
                            }
                            if (j - 1 > 0)
                            {
                                currentSpace = sideNum + i.ToString() + j.ToString();
                                NextSpace = sideNum + i.ToString() + (j - 1).ToString();
                                //if (NextSpace == "11" || NextSpace == "71" || NextSpace == "77" || NextSpace == "17") break;
                                // GameObject.Find(currentSpace).GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                                try
                                {
                                    if (currSpace.GetComponent<BoardSpaceController>())
                                    {
                                        currSpace.GetComponent<BoardSpaceController>().PossibleSides.Add(GameObject.Find(NextSpace));
                                    }
                                }
                                catch { print("board controller broke - adding the sides to the board tiles: " + currentSpace + " cant find " + NextSpace); }
                            }
                        }
                    }
                }
            }
        }
    }

    // go with same name cause conflicting issues since it gets wrong GO, have it run for each side
    //well that has been redone but now it needs to allow edge tiles to get from other board. ideas? - none. pain? - great. hope? - lost
}