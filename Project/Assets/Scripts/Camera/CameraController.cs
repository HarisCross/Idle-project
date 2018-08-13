using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private int screenWidth, screenHeight,movementSpeed = 5;
    public int edgeBoundary = 50;
    public float CameraHeight = 3f;
    private GameObject player;
    float timeTaken = 3f, currTime;
    Vector3 startPos,tPos,oldPos;
    List<GameObject> boxes = new List<GameObject>();
    public float RotSpeed = 3;

    float yaw = 0f, pitch = 0f;

    private Ray ray;
    private RaycastHit hit;

    public bool CameraLocked = false, moving = false, ViewingObject = false;

    Transform targetPos;
    private bool paused = false;
    private bool sideFocused = false;
    private UIInteraction uiInteraction;

    private void Awake()
    {
        uiInteraction = GameObject.Find("UI").GetComponent<UIInteraction>();
        player = GameObject.Find("Player");

        Cursor.lockState = CursorLockMode.Confined;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
       
    }
    public void PauseCamera(float tim)
    {
        paused = true;
        StartCoroutine(SpawnPause(tim));
    }
    IEnumerator SpawnPause(float tim)
    {
       // Debug.Log("Wait" + tim);
        yield return new WaitForSeconds(tim);
        paused = false;
    }
    // Use this for initialization
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        if (paused == false)
        {

            InputChecks();

            if (moving == true)
            {
                // MoveToPos();

                currTime += Time.deltaTime / timeTaken;
                transform.position = Vector3.Lerp(startPos, tPos, currTime);
                if (targetPos != null)
                {
                    Debug.Log("looking at" + targetPos.name);
                    if (targetPos.GetComponentInParent<BoxCollider>() != null)
                    {
                        transform.LookAt(targetPos.GetComponentInParent<BoxCollider>().transform);
                    }
                    else
                    {
                        transform.LookAt(targetPos);
                    }
                }
                else
                {
                    Debug.Log("target pos is empty");
                }
                if (Vector3.Distance(transform.position, tPos) < 0.1f)
                {

                    moving = false;
                }

            }

        }

    }
    public void MoveBack()
    {
        MoveBackToOldPosition(oldPos);
    }
    private void InputChecks()
    {
        if (Input.GetMouseButton(1) && CameraLocked == true)
        {

            //targetPos = transform;
            MoveBack();
            uiInteraction.DisableSideButtons();
        
        }


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetMouseButton(0))
        {
            ClickOnObject();


            if (CameraLocked == false)
            {
                MoveCamera();
            }
        }
        else if (Input.GetMouseButton(1) & CameraLocked == false)
        {
            RotateCamera();
        }
    }
    public void ConnectorTopFocused(GameObject Connector)
    {
        GameObject cameraPoint = Connector.transform.GetChild(1).GetChild(1).gameObject;
        targetPos = Connector.transform;
       // Debug.Log(cameraPoint.name);

        SetTargetPosition(cameraPoint.transform.position, Connector, Connector.transform.GetChild(1).gameObject);

    }
    private void ResetRotationForConnector()
    {
        //change rotate to have ui line up with connector below
        int countbreak = 0;
        do
        {
            if(player.transform.rotation.y < 0)
            {
                //less than 0 - inc rot


            }
            else
            {
                //more than 0 - dec rot


            }

            countbreak++;
        } while (player.transform.rotation.y != 0| countbreak == 100);


    }
    public void SetTargetPosition(Vector3 pos,GameObject focus,GameObject side)//moves camera to preset position relative to the item selected
    {

        currTime = 0;
        startPos = transform.position;
        timeTaken = 1f;
        tPos = pos;


        moving = true;
        CameraLocked = true;
        sideFocused = true;
        uiInteraction.boxSideFocused = side;
        BoxFocusedTrigger(focus);
        uiInteraction.ClickedOnSide();

    }
    public void MoveBackToOldPosition(Vector3 pos)//moves caerma to pos before clicking the item
    {

        currTime = 0;
        startPos = transform.position;
        timeTaken = 1f;
        tPos = pos;

      //  this.GetComponent<UIInteraction>().DisableUI();

        moving = true;
        CameraLocked = false;
        sideFocused = false;
        uiInteraction.boxSideFocused = null;
        BoxNotFocused();
    }
    void BoxFocusedTrigger(GameObject focus)//only shows the focued box, rest are invisbile
    {
        boxes.Clear();
        //get all boxes
        //set all invis
        //change one selected to vis

        boxes.AddRange(GameObject.FindGameObjectsWithTag("MainBox"));
        boxes.AddRange(GameObject.FindGameObjectsWithTag("BoxReciever"));
        boxes.AddRange(GameObject.FindGameObjectsWithTag("BoxConnector"));
        

        foreach (GameObject child in boxes)
        {

            //Debug.Log(focus.gameObject.name + " : "+ child.gameObject.name);


            if (focus != child) {
                MeshRenderer[] meshes = child.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer mesh in meshes)
                {
                    mesh.enabled = false;

                }

            }
            

        }


    }
    void BoxNotFocused()//shows all boxes again
    {
        boxes.Clear();

        //set all to active
        boxes.AddRange(GameObject.FindGameObjectsWithTag("MainBox"));
        boxes.AddRange(GameObject.FindGameObjectsWithTag("BoxReciever"));
        boxes.AddRange(GameObject.FindGameObjectsWithTag("BoxConnector"));


        foreach (GameObject child in boxes)
        {
                MeshRenderer[] meshes = child.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer mesh in meshes)
                {
                    mesh.enabled = true;

                }

            


        }

    }
    void ClickOnObject()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        if(Physics.Raycast(ray.origin, ray.direction,out hit, 50))
        {
            if (hit.transform.tag == "BoxSide" | hit.transform.tag == "ConnTop")
            {
                if (CameraLocked == false)
                {
                    oldPos = transform.position;
                    Debug.Log(hit.transform.gameObject.name);
                    if(hit.transform.tag == "ConnTop")
                    {
                        targetPos = hit.transform.parent.transform;
                    }
                    else
                    {
                        targetPos = hit.transform;
                    }
                   // targetPos = hit.transform;

                    SetTargetPosition(hit.transform.GetChild(1).transform.position, hit.transform.parent.transform.parent.gameObject, hit.transform.gameObject);

                    //this.GetComponent<UIInteraction>().UIBoxSideInteraction(true);
                }
            }
        }
    }
    void MoveCamera()
    {
        //Debug.Log("moving camera");

        //corners
        if (Input.mousePosition.x > (screenWidth - edgeBoundary) & Input.mousePosition.y > (screenHeight - edgeBoundary))//forward/right
        {
          //    Debug.Log("forward Right");

            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);
            
            return;
        }

        if (Input.mousePosition.x < 0 + edgeBoundary & Input.mousePosition.y > (screenHeight - edgeBoundary)) //forward/left
        {
           //  Debug.Log("forward Left");

            transform.Translate(-Vector3.right * movementSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }

        if (Input.mousePosition.y < 0 + edgeBoundary & Input.mousePosition.x > (screenWidth - edgeBoundary))//back/right
        {
           //  Debug.Log("back right");

            transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }

        if (Input.mousePosition.y < 0 + edgeBoundary & Input.mousePosition.x < 0 + edgeBoundary)//back/left
        {
           //  Debug.Log("back left");

            transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
            transform.Translate(-Vector3.right * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }





        //middle borders
        if (Input.mousePosition.x > (screenWidth - edgeBoundary))//right
        {
          //  Debug.Log("Right");

            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }

        if (Input.mousePosition.x < 0 + edgeBoundary)//left
        {
           // Debug.Log("Left");

            transform.Translate(-Vector3.right * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }

        if (Input.mousePosition.y > (screenHeight - edgeBoundary))//forward
        {
           // Debug.Log("forward");

            transform.Translate(Vector3.forward * movementSpeed* Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }

        if (Input.mousePosition.y < 0 + edgeBoundary)//back
        {
           // Debug.Log("back");

            transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, CameraHeight, transform.position.z);

            return;
        }

    }
    void RotateCamera()
    {

         yaw += RotSpeed * Input.GetAxis("Mouse X");
         pitch -= RotSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

}
