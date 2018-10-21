//using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    //public GameObject UIBoxSide,UI;

    private PlayerController player;
    private BoardController boardController;
    private TimeController timeController;
    private CameraController camController;
    public AudioManager audioManager;

    public GameObject Menu; private bool menuEnabled = true;
    public GameObject menuButton; private bool menuButtonEnabled;

    public GameObject ButtonsHolder;
    public GameObject DeleteSideButton;
    public GameObject DeleteBoxButton;
    public GameObject ConnSwapButton;
    public GameObject ConnButtons;
    public GameObject sideOptions;

    public GameObject connStatusGO;
    public Text ConnStatusText;

    public bool menuDisplayed = false, moveMenu = false;
    private float currTime;
    private float timeTaken = 0.5f;
    private Vector3 startPos;
    private Vector3 tPos;
    public GameObject tempGO;

    // public bool BoxOrConn = true; // true for box, false for conn
    public int BoxConnGen = 0;//1 box,2 conn,3 gen

    //  private Text ErrorText;
    private Ray ray;

    private RaycastHit hit;

    public GameObject boxSideFocused;//the numbered side with controller
    public GameObject side;//side object spawned in
    private GameObject sideLoc;

    [SerializeField]
    public bool PurchasingTiles = false;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boardController = GameObject.Find("BoardGrid").GetComponent<BoardController>();
        //ErrorText = GameObject.Find("ErrorDisplayText").GetComponent<Text>();
        timeController = GameObject.Find("TimeController").GetComponent<TimeController>();
        camController = GameObject.Find("Player").GetComponent<CameraController>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckUIDisplay();
        ButtonUpdate();
    }

    public void ClickedOnSide()
    {
        if (boxSideFocused != null & boxSideFocused.transform.childCount >= 3)
        {
            side = boxSideFocused.transform.GetChild(1).gameObject;
        }
    }

    private void ButtonUpdate()
    {
        if (boxSideFocused != null)
        {
            if (boxSideFocused.tag == "ConnTop")
            {
                //Debug.Log("looking at connector top");

                ConnButtons.GetComponent<ConnButtonController>().Active = true;
                ConnButtons.GetComponent<ConnButtonController>().ConnTarget = boxSideFocused.transform.parent.transform.gameObject; //is giving conntop/ chagne to give connector itself

                if (ConnButtons.GetComponent<ConnButtonController>().ButtonsActive == false)
                {
                    ConnButtons.GetComponent<ConnButtonController>().ActivateButtons();
                    ConnButtons.GetComponent<ConnButtonController>().ButtonsActive = true;
                }
            }
            else
            {
                ConnButtons.GetComponent<ConnButtonController>().Active = false;
                ConnButtons.GetComponent<ConnButtonController>().DeactivateButtons();

                // ConnButtons.GetComponent<ConnButtonController>().ConnTarget = null; //is giving conntop/ chagne to give connector itself
            }
            if (boxSideFocused.transform.parent.parent.tag == "MainBox")
            {
                //  print("set true");
                sideOptions.SetActive(true);
            }
            else
            {
                sideOptions.SetActive(false);
            }
        }
        else
        {
            sideOptions.SetActive(false);
            ConnButtons.GetComponent<ConnButtonController>().Active = false;

            if (ConnButtons.GetComponent<ConnButtonController>().ButtonsActive == true)
            {
                ConnButtons.GetComponent<ConnButtonController>().DeactivateButtons();
                ConnButtons.GetComponent<ConnButtonController>().ButtonsActive = false;
            }
        }

        //if looking at box then show box delete
        //if looking at side then show side delete

        if (side != null)
        {
            DeleteSideButton.SetActive(true);
            //  Debug.Log(side.gameObject.name);

            if (boxSideFocused.GetComponent<BoxSideController>().side == "Connector")
            {
                if (boxSideFocused.transform.parent.parent.tag != "BoxReciever")
                {
                    ConnSwapButton.SetActive(true);
                }
                // ConnSwapButton.SetActive(true);
                connStatusGO.SetActive(true);
                sideOptions.SetActive(false);
            }
            else
            {
                ConnSwapButton.SetActive(false);
                connStatusGO.SetActive(false);
            }
        }
        else
        {
            ConnSwapButton.SetActive(false);
            DeleteSideButton.SetActive(false);
        }

        if (ConnStatusText.IsActive() & boxSideFocused)
        {
            switch (boxSideFocused.GetComponent<BoxSideController>().connectorStatus)
            {
                case 0:
                    //not connector
                    Debug.Log("somehow connector hasnt been assigned as a connector");
                    break;

                case 1:
                    //exp side
                    ConnStatusText.text = "ConnStatus: Export";

                    break;

                case 2:
                    ConnStatusText.text = "ConnStatus: Input";

                    break;

                default:

                    break;
            }
        }
        else { connStatusGO.SetActive(false); }

        if (boxSideFocused != null && side == null)
        {
            DeleteBoxButton.SetActive(true);
        }
        else
        {
            DeleteBoxButton.SetActive(false);
        }
    }

    public void DisableSideButtons()
    {
        side = null;
        //DeleteSideButton.SetActive(false);
    }

    private void CheckUIDisplay()
    {
        if (boxSideFocused == null)
        {
            ButtonsHolder.SetActive(true);
            // boardController.CloseActiveDisplay();
        }
        else
        {
            ButtonsHolder.SetActive(false);
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (GameObject.Find("BoardGrid").GetComponent<BoardController>().DisplayAvail == false)
        {
            Object.Destroy(tempGO);
        }

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 50))
        {
            if (PurchasingTiles)
            {
                if (hit.transform.tag != "BoardSpace") return;
                AttemptToBuyTile(hit.transform.gameObject);
                return;
            }
            if (hit.transform.gameObject.tag == "BoardSpace")
            {
                switch (BoxConnGen)
                {
                    case 1:

                        if (boardController.DisplayAvail == true)
                        {
                            if (hit.transform.GetComponent<BoardSpaceController>().TakenByB == false /*& hit.transform.GetComponent<BoardSpaceController>().TakeByS == false*/)
                            {
                                if (tempGO != null)
                                {
                                    if (tempGO.transform.gameObject.name != hit.transform.gameObject.name)
                                    {
                                        Object.Destroy(tempGO);
                                        tempGO = null;
                                    }
                                }
                                else
                                {
                                }
                                tempGO = Instantiate(Resources.Load("TempBox", typeof(GameObject))) as GameObject;
                                tempGO.transform.position = hit.transform.GetChild(1).gameObject.transform.position;
                            }
                            if (EventSystem.current.IsPointerOverGameObject()) return;
                            if (hit.transform.GetComponent<BoardSpaceController>().Purchased == false) return;
                            if (Input.GetMouseButtonDown(0))
                            {
                                SpawnBox(hit);
                            }
                        }

                        break;

                    case 2:

                        if (boardController.DisplayAvail == true)
                        {
                            if (hit.transform.GetComponent<BoardSpaceController>().TakenByB == false /*& hit.transform.GetComponent<BoardSpaceController>().TakeByS == false*/)
                            {
                                if (tempGO != null)
                                {
                                    if (tempGO.transform.gameObject.name != hit.transform.gameObject.name)
                                    {
                                        Object.Destroy(tempGO);
                                        tempGO = null;
                                    }
                                }
                                else
                                {
                                }
                                tempGO = Instantiate(Resources.Load("TempConn", typeof(GameObject))) as GameObject;
                                tempGO.transform.position = hit.transform.GetChild(1).gameObject.transform.position;
                            }
                            if (EventSystem.current.IsPointerOverGameObject()) return;
                            if (hit.transform.GetComponent<BoardSpaceController>().Purchased == false) return;
                            if (Input.GetMouseButtonDown(0))
                            {
                                SpawnConnector(hit);
                            }

                            break;
                        }
                        break;

                    case 3:

                        if (boardController.DisplayAvail == true)
                        {
                            if (hit.transform.GetComponent<BoardSpaceController>().TakenByB == false /*& hit.transform.GetComponent<BoardSpaceController>().TakeByS == false*/)
                            {
                                if (tempGO != null)
                                {
                                    if (tempGO.transform.gameObject.name != hit.transform.gameObject.name)
                                    {
                                        Object.Destroy(tempGO);
                                        tempGO = null;
                                    }
                                }
                                else
                                {
                                }
                                tempGO = Instantiate(Resources.Load("TempGen", typeof(GameObject))) as GameObject;
                                tempGO.transform.position = hit.transform.GetChild(1).gameObject.transform.position;
                            }
                            if (EventSystem.current.IsPointerOverGameObject()) return;
                            if (hit.transform.GetComponent<BoardSpaceController>().Purchased == false) return;
                            if (Input.GetMouseButtonDown(0))
                            {
                                //SpawnConnector(hit);
                                SpawnGenerator(hit);
                            }

                            break;
                        }

                        break;
                }
            }
        }
    }

    private void SpawnBox(RaycastHit hitInfo)
    {
        //set board spaces to work
        //check if player can buy if so then do else dont and display
        //spawn empty object on pos

        if (player.Money > 50)
        {
            player.Money -= 50f;

            boardController.DisplayFreeBoxSpacesBox();

            Object.Destroy(tempGO);
            GameObject Box;

            Box = Instantiate(Resources.Load("BaseBox1", typeof(GameObject))) as GameObject;
            Box.transform.position = hitInfo.transform.GetChild(1).gameObject.transform.position;

            // Debug.Log(Box.gameObject.name + Box.transform.position);

            hit.transform.gameObject.GetComponent<BoardSpaceController>().CurrentBox = Box;
            hit.transform.gameObject.GetComponent<BoardSpaceController>().TakenByB = true;
            Box.transform.parent = GameObject.Find("BoxHolder").transform;

            player.gameObject.GetComponent<CameraController>().PauseCamera(0.5f);

            // ErrorText.text = "";

            timeController.UpdateAddList(Box);

            Box.GetComponent<BoxController>().SpaceTaken = hit.transform.gameObject;

            audioManager.ObjectConstruction(Box);

            // RotateForSide(Box, hitInfo.transform.parent.transform.parent.GetComponent<BoardBoxSide>().boardBoxSide);
            RotateForSide(Box, hitInfo.transform./*parent.transform.parent.*/gameObject);
        }
        else
        {
            //not enough money
            //  ErrorText.text = "UI - Not enough money";
        }
    }

    private void RotateForSide(GameObject pass,/*int side*/GameObject tile)
    {
        //rotate the object placed so it fits the side

        // print(pass.name + " on " + side);

        //switch (side)
        //{
        //     case 0:
        //        //none needed
        //        break;
        //     case 1:
        //        pass.transform.Rotate(-90,0, 0, Space.Self);
        //        break;
        //     case 2:
        //        pass.transform.Rotate(0, 0, 90, Space.Self);
        //        break;
        //     case 3:
        //        pass.transform.Rotate(0, 0, -90, Space.Self);
        //        break;
        //     case 4:
        //        pass.transform.Rotate(90, 0, 0, Space.Self);
        //        break;
        //     case 5:
        //        pass.transform.Rotate(180, 0, 0, Space.Self);
        //        break;

        //}

        //change to check tile up direction and rotate pass to it

        pass.transform.rotation = tile.transform.rotation;//better way of doing the above, allows for easier positioning of tile and objects.
    }

    public void PurchaseBoardTile()
    {
        //show tiles

        // if user clicks on tile while purcahsing is active then do something

        PurchasingTiles = !PurchasingTiles;
        boardController.DisplayBoardTiles();
    }

    private void AttemptToBuyTile(GameObject tile)
    {
        //activated if user clicks on tile while purchasing is true

        //purchase - check cost, change purchased
        //deactivate cost for tile bought
        //  print(tile.name);

        int cost = tile.GetComponent<BoardSpaceController>().SpaceCost;
        if (Input.GetMouseButtonDown(0))
        {
            if (player.Money > cost && tile.GetComponent<BoardSpaceController>().Purchased == false)
            {
                //can afford tile
                // tile.GetComponent<BoardSpaceController>().Purchased = true;
                tile.GetComponent<BoardSpaceController>().DisableTextPrice();

                player.Money -= cost;
                print("bought: " + tile.name);
            }
            else
            {
                //cant afford tile
                print("cant afford tile or tile already owned" + player.Money + " : " + cost);
            }
        }
    }

    private void SpawnConnector(RaycastHit hitInfo)
    {
        int price = 100;
        if (player.Money > price)
        {
            player.Money -= price;

            boardController.DisplayFreeBoxSpacesBox();

            Object.Destroy(tempGO);
            GameObject Box;

            Box = Instantiate(Resources.Load("Connector", typeof(GameObject))) as GameObject;
            Box.transform.position = hitInfo.transform.GetChild(1).gameObject.transform.position;

            // Debug.Log(Box.gameObject.name + Box.transform.position);

            hit.transform.gameObject.GetComponent<BoardSpaceController>().CurrentBox = Box;
            hit.transform.gameObject.GetComponent<BoardSpaceController>().TakenByB = true;
            Box.transform.parent = GameObject.Find("ConnHolder").transform;

            player.gameObject.GetComponent<CameraController>().PauseCamera(0.5f);

            //  ErrorText.text = "";

            timeController.UpdateAddList(Box);

            Box.GetComponent<ConnectorController>().SpaceTaken = hit.transform.gameObject;

            //  camController.ConnectorTopFocused(Box);

            foreach (GameObject side in Box.GetComponent<ConnectorController>().ConnectorSides)
            {
                Box.GetComponent<ConnectorController>().ImpExpSides.Add(side);

                // .ImpExpSides.Add(sideController.gameObject);
            }
            audioManager.ObjectConstruction(Box);
            RotateForSide(Box, hitInfo.transform.gameObject);
        }
        else
        {
            //not enough money
            //ErrorText.text = "UI - Not enough money";
        }
    }

    private void SpawnGenerator(RaycastHit hitInfo)
    {
        int price = 100;
        if (player.Money > price)
        {
            player.Money -= price;

            boardController.DisplayFreeBoxSpacesBox();

            Object.Destroy(tempGO);
            GameObject Box;

            Box = Instantiate(Resources.Load("Generator", typeof(GameObject))) as GameObject;
            Box.transform.position = hitInfo.transform.GetChild(1).gameObject.transform.position;

            // Debug.Log(Box.gameObject.name + Box.transform.position);

            hit.transform.gameObject.GetComponent<BoardSpaceController>().CurrentBox = Box;
            hit.transform.gameObject.GetComponent<BoardSpaceController>().TakenByB = true;
            Box.transform.parent = GameObject.Find("BoxHolder").transform;

            player.gameObject.GetComponent<CameraController>().PauseCamera(0.5f);

            //  ErrorText.text = "";

            timeController.UpdateAddList(Box);

            Box.GetComponent<GeneratorController>().SpaceTaken = hit.transform.gameObject;

            //  camController.ConnectorTopFocused(Box);

            //////foreach (GameObject side in Box.GetComponent<ConnectorController>().ConnectorSides)
            //////{
            //////    Box.GetComponent<ConnectorController>().ImpExpSides.Add(side);

            //////    // .ImpExpSides.Add(sideController.gameObject);
            //////}
            ///

            if (Box.GetComponent<GeneratorController>().ConnSide != null)
            {
                Box.GetComponent<GeneratorController>().ImpExpSides.Add(Box.GetComponent<GeneratorController>().ConnSide);
            }

            audioManager.ObjectConstruction(Box);
            //RotateForSide(Box, hitInfo.transform.gameObject);
        }
        else
        {
            //not enough money
            //ErrorText.text = "UI - Not enough money";
        }
    }

    public void SideListDisplay()
    {
        //show side option list - move to bottom of heirarchy
    }

    public void SpawnSide1()
    {
        //  make if to check if side already present
        if (boxSideFocused != null)
        {
            if (boxSideFocused.GetComponent<BoxSideController>().boxSidePresent == false)
            {
                string sideName = "BoxSide1";//which side prefab to spawn in
                int sideNumber = boxSideFocused.GetComponent<BoxSideController>().SideNumber;
                boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = true;
                SpawnSideMain(sideName, sideNumber);
            }
        }
    }

    public void SpawnSide2()
    {
        //  make if to check if side already present
        if (boxSideFocused != null)
        {
            if (boxSideFocused.GetComponent<BoxSideController>().boxSidePresent == false)
            {
                string sideName = "BoxSide2";//which side prefab to spawn in
                int sideNumber = boxSideFocused.GetComponent<BoxSideController>().SideNumber;
                boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = true;
                SpawnSideMain(sideName, sideNumber);
            }
        }
    }

    public void SpawnSide3()
    {        //  make if to check if side already present
        if (boxSideFocused != null)
        {
            if (boxSideFocused.GetComponent<BoxSideController>().boxSidePresent == false)
            {
                string sideName = "BoxSide3";//which side prefab to spawn in
                int sideNumber = boxSideFocused.GetComponent<BoxSideController>().SideNumber;
                boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = true;
                SpawnSideMain(sideName, sideNumber);
            }
        }
    }

    public void SpawnSide4()
    {        //  make if to check if side already present
        if (boxSideFocused != null)
        {
            if (boxSideFocused.GetComponent<BoxSideController>().boxSidePresent == false)
            {
                string sideName = "BoxSideConnector";//which side prefab to spawn in
                int sideNumber = boxSideFocused.GetComponent<BoxSideController>().SideNumber;
                boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = true;
                SpawnSideMain(sideName, sideNumber);
            }
        }
    }

    public void SwapConnStatus()
    {
        //swap connector from inp <-> exp and refresh lists

        if (boxSideFocused.GetComponent<BoxSideController>().connectorStatus == 1)
        {
            boxSideFocused.GetComponent<BoxSideController>().connectorStatus = 2;
            //Debug.Log("Conn status changed to 2");
        }
        else
        {
            boxSideFocused.GetComponent<BoxSideController>().connectorStatus = 1;
            // Debug.Log("Conn status changed to 1");
        }

        if (boxSideFocused.transform.parent.parent.tag == "BoxReciever")
        {
        }
        else
        {
            boxSideFocused.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().UpdateInpExpLists();
        }

        //   boxSideFocused.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().UpdateInpExpLists();
    }

    public void DeleteSide()
    {
        // Debug.Log(boxSideFocused.name);
        boxSideFocused.transform.GetChild(0).gameObject.SetActive(true);
        boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = false;
        boxSideFocused.GetComponent<BoxSideController>().IncomeHeld = 0;
        boxSideFocused.GetComponent<BoxSideController>().IncomeRate = 0;

        //  timeController.UpdateRemoveList(boxSideFocused.transform.GetChild(2).transform.gameObject);

        timeController.UpdateRemoveList(boxSideFocused);

        if (boxSideFocused.GetComponent<BoxSideController>().side == "Connector")
        {
            //if the side is a connector
            boxSideFocused.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().ImpExpSides.Remove(boxSideFocused.GetComponent<BoxSideController>().gameObject);
            boxSideFocused.GetComponent<BoxSideController>().connectorStatus = 1;

            boxSideFocused.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().UpdateInpExpLists();
            boxSideFocused.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().RemoveNearTile(boxSideFocused);
        }

        //sideLoc.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().UpdateLists();
        audioManager.ObjectDeconstruction(boxSideFocused);
        Destroy(boxSideFocused.transform.GetChild(2).gameObject);

        side = null;

        //reactivate box//
        //set all box side details on controller to null
        //set box side present to false//
        //delete the side spawned in
    }

    public void DeleteBox()
    {
        GameObject box = boxSideFocused.transform.parent.parent.transform.gameObject;
        //``` audioManager.ObjectDeconstruction(box);

        if (box.name == "ConnHolder")
        {
            box = boxSideFocused.transform.parent.transform.gameObject;
        }

        // Debug.Log(box.name);

        //update board grid!!!!!!!!!!!!!!!!!!!

        timeController.UpdateRemoveList(box);

        if (box.GetComponent<BoxController>())
        {
            box.GetComponent<BoxController>().SpaceTaken.GetComponent<BoardSpaceController>().TakenByB = false;
        }
        else
        {
            //  print(box.name);
            box.GetComponent<ConnectorController>().SpaceTaken.GetComponent<BoardSpaceController>().TakenByB = false;
        }

        //updater the spaces bool values
        Destroy(box);

        boardController.UpdatePossBOSList();
        camController.MoveBack();
        //sideLoc.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().UpdateLists();
    }

    private void SpawnSideMain(string chosenSide, int SideNumber)
    {
        //get side chosen
        //deactivate box
        //spawn side specified on side with rotation
        //get box side details from boxed spawned and pass to box side controller

        //side; // side spawned in
        sideLoc = boxSideFocused; // the side attached too

        //  Debug.Log(boxSideFocused.name);
        side = Instantiate(Resources.Load(chosenSide, typeof(GameObject))) as GameObject;
        //  side = side.transform.GetChild(0).gameObject;
        side.transform.position = sideLoc.transform.position;
        side.transform.parent = sideLoc.transform;
        boxSideFocused.transform.GetChild(0).gameObject.SetActive(false);

        audioManager.ObjectConstruction(boxSideFocused.transform.parent.gameObject);
        //switch to rotate side to display properly on each side

        ///## ###i have no clue why changing the X axis makes it rotate around the Y axisd like wanted but meh

        //  Debug.Log(side.gameObject.name);
        timeController.UpdateAddList(sideLoc);

        BoxSideDetails details = side.transform.GetChild(0).GetComponent<BoxSideDetails>();
        BoxSideController sideController = sideLoc.GetComponent<BoxSideController>();

        if (sideController == null) Debug.Log("SC empty");
        print(details.transform.gameObject.name);
        print(sideLoc.transform.gameObject.name);
        //print(sideController.IncomeRate + " : " + details.IncomeRate);

        sideController.IncomeRate = details.IncomeRate;
        sideController.IncomeHeld = details.IncomeHeld;
        // sideController.sideConnector = details.sideConnector;
        sideController.side = details.side;

        if (sideController.side == "Connector")
        {
            //if the side is a connector
            sideController.MainBox.GetComponent<BoxController>().ImpExpSides.Add(sideController.gameObject);
            boxSideFocused.GetComponent<BoxSideController>().connectorStatus = 1;

            sideController.MainBox.GetComponent<BoxController>().UpdateInpExpLists();
            sideController.MainBox.GetComponent<BoxController>().UpdateConnObj();
        }

        //RotateBoxSide(SideNumber, side.transform.GetChild(0).gameObject,sideLoc.transform.parent.transform.parent.gameObject);

        // sideController.MainBox.GetComponent<BoxController>().UpdateLists();
    }

    private void RotateBoxSide(int SideNumber, GameObject sidePassed, GameObject boxParent)
    {
        print(sidePassed.name + " passed to rotate func");
        GameObject camPoint = sidePassed.transform.parent.transform.parent.GetChild(1).gameObject;
        GameObject lookAtPoint = boxParent.transform.GetChild(2).transform.gameObject;
        // Vector3 newRot;

        //Vector3 dir = this.transform.position - MainBox.transform.position;

        GameObject sideAdded = sidePassed.transform.parent.gameObject;
        Vector3 dir = sideAdded.transform.position - boxParent.transform.position;
        print("parent is: " + boxParent);

        sideAdded.transform.LookAt(dir);

        // sidePassed.transform.LookAt(2*sidePassed.transform.position- boxParent.transform.position);
        //Quaternion.Inverse(sidePassed.transform.rotation);

        //sidePassed.transform.LookAt(camPoint.transform.position);

        switch (SideNumber)//sides go clockwise with 5 ontop
        {
            case 1://

                //  newRot = sidePassed.transform.rotation.eulerAngles;
                //  newRot = new Vector3(newRot.x, newRot.y + 90, newRot.z);
                // side.transform.rotation = Quaternion.Euler(newRot);

                Debug.Log("Side 1");
                break;

            case 2://y+90 turn right 90

                // newRot = sidePassed.transform.rotation.eulerAngles;
                //newRot = new Vector3(newRot.x, newRot.y + 90, newRot.z);
                //side.transform.rotation = Quaternion.Euler(newRot);

                Debug.Log("Side 2");

                break;

            case 3://y+180 turn left 90

                //newRot = sidePassed.transform.rotation.eulerAngles;
                //newRot = new Vector3(newRot.x, newRot.y +180, newRot.z);
                //side.transform.rotation = Quaternion.Euler(newRot);

                Debug.Log("Side 3");

                break;

            case 4://y-90 turn either 180

                // newRot = sidePassed.transform.rotation.eulerAngles;
                //newRot = new Vector3(newRot.x, newRot.y -90, newRot.z);
                //side.transform.rotation = Quaternion.Euler(newRot);

                //  side.transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                //sidePassed.transform.Rotate(-90, 0, 0, Space.Self);

                Debug.Log("Side 4");

                break;

            case 5://x-90 turn upward 90

                //newRot = sidePassed.transform.rotation.eulerAngles;
                //newRot = new Vector3(newRot.x, newRot.y, newRot.z-90);
                //side.transform.rotation = Quaternion.Euler(newRot);

                Debug.Log("Side 5");

                break;

            default:
                //should never reach this
                Debug.Log("switch - sdie number broken - UIinteraction");
                break;
        }
    }

    public void MenuEnableDisable()
    {
        Menu.SetActive(menuEnabled);
        menuEnabled = !menuEnabled;

        menuButton.SetActive(menuButtonEnabled);
        menuButtonEnabled = !menuButtonEnabled;
    }

    public void FullscreenFlip()
    {
        //if (Screen.fullScreen == false)
        //{
        //    //if fullscreen
        //    Screen.fullScreenMode = FullScreenMode.Windowed;
        //    Screen.fullScreen = true;
        //    Debug.Log("swap to windowed");
        //}
        //else
        //{
        //    Debug.Log("spawn to fullscreen");
        //    Screen.fullScreen = false;
        //    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        //}

        // Screen.fullScreenMode = !Screen.fullScreen;
        // print("screen is: " +Screen.fullScreen);
    }
}