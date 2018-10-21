using System.Collections.Generic;
using UnityEngine;

public class BoardSpaceController : MonoBehaviour
{
    public int x, y;

    public bool TakenByB = false;
    public bool allocatedNumbersComplete = false;
    //  public bool TakeByS = false;

    public GameObject CurrentBox;
    public BoardController boardController;
    public GameObject[] adjConnectorMANUAL;

    public List<GameObject> PossibleSides;
    public List<GameObject> PossibleSideOpenSpots;
    private List<GameObject> toDelete = new List<GameObject>();

    public bool displayTextPrice;
    public bool Purchased = false;
    public int SpaceCost = 50;

    public GameObject TextPrice;

    private void Awake()
    {
        AllocateNumbers();
    }

    // Use this for initialization
    private void Start()
    {
        // if (adjConnectorMANUAL != null) PossibleSides.Add(adjConnectorMANUAL);

        foreach (GameObject tile in adjConnectorMANUAL)
        {
            PossibleSides.Add(tile);
        }

        foreach (GameObject side in PossibleSides)
        {
            if (side == null) toDelete.Add(side);
        }

        foreach (GameObject side in toDelete)
        {
            PossibleSides.Remove(side);
        }

        TextPrice = gameObject.transform.GetChild(2).transform.gameObject;
        boardController = GameObject.Find("BoardGrid").GetComponent<BoardController>();
        if (Purchased == false)
        {
            //set red if false and green if bought

            gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void DisableTextPrice()
    {
        displayTextPrice = false;
        Purchased = true;
        gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().material.color = Color.green;
        TextPrice.SetActive(false);
    }

    public void ToggleCloseText()
    {
        displayTextPrice = false;
        TextPrice.SetActive(false);
    }

    public void TogglePriceDisplay(bool view)
    {
        displayTextPrice = !displayTextPrice;
        if (view)
        {
            if (displayTextPrice == true && TakenByB == false && Purchased == false && displayTextPrice == true) TextPrice.SetActive(true);
            if (displayTextPrice == false) TextPrice.SetActive(false);
        }
        if (Purchased == false)
        {
            //set red if false and green if bought

            gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            gameObject.transform.GetChild(0).transform.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }

    private void AllocateNumbers()
    {
        string name = gameObject.name;

        char xChar = name[0];
        char yChar = name[1];

        // x = int.Parse(name[]);

        x = (int)char.GetNumericValue(xChar);
        y = (int)char.GetNumericValue(yChar);

        int side = gameObject.transform.parent.transform.parent.GetComponent<BoardBoxSide>().boardBoxSide;
        gameObject.name = side.ToString() + name;
        allocatedNumbersComplete = true;
        //boardController.UpdateBoardSpaceControllerSidesList();
    }
}