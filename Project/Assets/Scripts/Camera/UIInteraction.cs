using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour {

    //public GameObject UIBoxSide,UI;

    private PlayerController player;
    private BoardController boardController;
    private TimeController timeController;
    public GameObject DeleteSideButton;
    public GameObject DeleteBoxButton;
    

    public bool menuDisplayed = false, moveMenu = false;
    private float currTime;
    private float timeTaken = 0.5f;
    private Vector3 startPos;
    private Vector3 tPos;
    public GameObject tempGO ;

    private Text ErrorText;
    private Ray ray;
    private RaycastHit hit;

    public GameObject boxSideFocused;
    GameObject side;
    GameObject sideLoc;

    void Start() {


        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boardController = GameObject.Find("BoardGrid").GetComponent<BoardController>();
        ErrorText = GameObject.Find("ErrorDisplayText").GetComponent<Text>();
        timeController = GameObject.Find("TimeController").GetComponent<TimeController>();
    }

    // Update is called once per frame
    void Update() {
        CheckUIDisplay();

        //if looking at box then show box delete
        //if looking at side then show side delete



        if(side != null)
        {
            DeleteSideButton.SetActive(true);
        }
        else
        {
            DeleteSideButton.SetActive(false);
        }

        if(boxSideFocused != null && side == null)
        {
            DeleteBoxButton.SetActive(true);
        }
        else
        {
            DeleteBoxButton.SetActive(false);

        }


    }
    private void CheckUIDisplay()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (GameObject.Find("BoardGrid").GetComponent<BoardController>().DisplayAvail == false)
        {
            Object.Destroy(tempGO);
        }

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 50))
        {


            switch (hit.transform.gameObject.tag)
            {
                case "BoardSpace":

                    if (boardController.DisplayAvail == true)
                    {
                        if (hit.transform.GetComponent<BoardSpaceController>().TakenByB == false & hit.transform.GetComponent<BoardSpaceController>().TakeByS == false)
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

                        if (Input.GetMouseButtonDown(0)){

                            SpawnBox(hit);
                        }


                         

                    }


                    break;
                default:
                    break;

            }

        }



    }
    void SpawnBox(RaycastHit hitInfo)
    {

        //set board spaces to work
        //check if player can buy if so then do else dont and display
        //spawn empty object on pos

        if (player.Money > 50)
        {
            player.Money -= 50f;

            boardController.DisplayFreeBoxSpaces();

            Object.Destroy(tempGO);
            GameObject Box;

            Box = Instantiate(Resources.Load("BaseBox1", typeof(GameObject))) as GameObject;
            Box.transform.position = hitInfo.transform.GetChild(1).gameObject.transform.position;

           // Debug.Log(Box.gameObject.name + Box.transform.position);

            hit.transform.gameObject.GetComponent<BoardSpaceController>().CurrentBox = Box;
            hit.transform.gameObject.GetComponent<BoardSpaceController>().TakenByB = true;
            Box.transform.parent = GameObject.Find("BoxHolder").transform;

            player.gameObject.GetComponent<CameraController>().PauseCamera(0.5f);

            ErrorText.text = "";

            timeController.UpdateAddList(Box);

            Box.GetComponent<BoxController>().SpaceTaken = hit.transform.gameObject;

        }
        else
        {
            //not enough money
            ErrorText.text = "UI - Not enough money";

        }



    }
   
    public void SideListDisplay()
    {
        //show side option list - move to bottom of heirarchy



    }
    public void SpawnSide1()
    {

        //  make if to check if side already present
        if(boxSideFocused != null)
        {
            if(boxSideFocused.GetComponent<BoxSideController>().boxSidePresent == false)
            {
                string sideName = "BoxSide1";//which side prefab to spawn in
                int sideNumber = boxSideFocused.GetComponent<BoxSideController>().SideNumber;
                boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = true;
                SpawnSideMain(sideName,sideNumber);
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
    public void DeleteSide()
    {
       // Debug.Log(boxSideFocused.name);
         boxSideFocused.transform.GetChild(0).gameObject.SetActive(true);
         boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = false;
        boxSideFocused.GetComponent<BoxSideController>().IncomeHeld = 0;
        boxSideFocused.GetComponent<BoxSideController>().IncomeRate = 0;

        //  timeController.UpdateRemoveList(boxSideFocused.transform.GetChild(2).transform.gameObject);

        timeController.UpdateRemoveList(boxSideFocused);




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
        Debug.Log(box.name);

        //update board grid!!!!!!!!!!!!!!!!!!!

        timeController.UpdateRemoveList(box);

        box.GetComponent<BoxController>().SpaceTaken.GetComponent<BoardSpaceController>().TakenByB = false;
        //updater the spaces bool values
        Destroy(box);

        boardController.UpdatePossBOSList();

    }
    private void SpawnSideMain(string chosenSide,int SideNumber)
    {

        //get side chosen
        //deactivate box
        //spawn side specified on side with rotation
        //get box side details from boxed spawned and pass to box side controller

         //side; // side spawned in
         sideLoc = boxSideFocused; // the side attached too

      //  Debug.Log(boxSideFocused.name);
        side = Instantiate(Resources.Load(chosenSide, typeof(GameObject))) as GameObject;
        side.transform.position = sideLoc.transform.position;
        side.transform.parent = sideLoc.transform;
        boxSideFocused.transform.GetChild(0).gameObject.SetActive(false);

        //switch to rotate side to display properly on each side

        ///#####i have no clue why changing the X axis makes it rotate around the Y axisd like wanted but meh
        switch (SideNumber)
        {
            case 1://y+90
                     side.transform.Rotate(90, 0, 0,Space.Self);
                   // side.transform.Rotate(Vector3.up * 90);
               
           // Debug.Log("Side 1");
            break;
            case 2://y+180
                side.transform.Rotate(180, 0, 0, Space.Self);

          //  Debug.Log("Side 2");

            break;
            case 3://y-90
                side.transform.Rotate(-90, 0, 0, Space.Self);

           // Debug.Log("Side 3");

            break;
            case 4://fine
                side.transform.Rotate(0, 0, 0, Space.Self);

           // Debug.Log("Side 4");

            break;
            case 5://z-90
                side.transform.Rotate(0, 0, -90, Space.Self);//top

         //   Debug.Log("Side 5");

            break;
            default:
                //should never reach this
                Debug.Log("switch - sdie number broken - UIinteraction");
            break;


        }

      //  Debug.Log(side.gameObject.name);
        timeController.UpdateAddList(sideLoc);


        BoxSideDetails details = side.GetComponent<BoxSideDetails>();
        BoxSideController sideController = sideLoc.GetComponent<BoxSideController>();

        if (sideController == null) Debug.Log("SC empty");

        sideController.IncomeRate = details.IncomeRate;
        sideController.IncomeHeld = details.IncomeHeld;


    }
}
