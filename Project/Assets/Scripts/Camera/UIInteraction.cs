using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour {

    //public GameObject UIBoxSide,UI;

    private PlayerController player;
    private BoardController boardController;

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

    void Start() {


        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boardController = GameObject.Find("BoardGrid").GetComponent<BoardController>();
        ErrorText = GameObject.Find("ErrorDisplayText").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update() {
        CheckUIDisplay();


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


        }
        else
        {
            //not enough money
            ErrorText.text = "UI - Not enough money";

        }



    }
    //private void MovingMenu()
    //{


    //    currTime += Time.deltaTime / timeTaken;
    //    transform.position = Vector3.Lerp(startPos, tPos, currTime);

    //    if (Vector3.Distance(transform.position, tPos) < 0.1f)
    //    {

    //        moveMenu = false;
    //    }


    //}
    //public void Showmenu()
    //{

    //    if(menuDisplayed == true)
    //    {
    //        //move  menu back

    //        startPos = this.transform.position;
    //        currTime = 0;
    //        tPos = this.transform.position;
    //        tPos.x += 350;

    //        moveMenu = true;
    //        menuDisplayed = false;
    //    }
    //    else
    //    {
    //        //move menu out

    //        startPos = this.transform.position;
    //        tPos = this.transform.position;
    //        currTime = 0;
    //        tPos.x -= 350;

    //        moveMenu = true;
    //        menuDisplayed = true;
    //    }

    //}
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
                string sideName = "BoxSide1";
                boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = true;
                SpawnSideMain(sideName);
            }
        }

    }
    public void SpawnSide2()
    {

    }
    public void SpawnSide3()
    {

    }
    public void DeleteSide()
    {
        boxSideFocused.transform.GetChild(0).gameObject.SetActive(true);
        boxSideFocused.GetComponent<BoxSideController>().boxSidePresent = false;
        //reactivate box//
        //set all box side details on controller to null
        //set box side present to false//
    }
    private void SpawnSideMain(string chosenSide)
    {

        //get side chosen
        //deactivate box
        //spawn side specified on side with rotation
        //get box side details from boxed spawned and pass to box side controller

        GameObject side;
        GameObject sideLoc = boxSideFocused;

        Debug.Log(boxSideFocused.name);
        side = Instantiate(Resources.Load(chosenSide, typeof(GameObject))) as GameObject;
        side.transform.position = sideLoc.transform.position;
        side.transform.parent = sideLoc.transform;

        sideLoc.transform.Rotate(0, -90, 0);

        boxSideFocused.transform.GetChild(0).gameObject.SetActive(false);



    }
}
