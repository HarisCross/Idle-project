using UnityEngine;
using UnityEngine.UI;

public class ConnectorSide : MonoBehaviour
{
    public string side = "Connector";

    [SerializeField]
    private ConnectorController CConn;

    private GameObject MainController;
    private PlayerController playerCon;
    public GameObject buttonAssigned;
    private GameObject thisSide;
    public bool sidePresent = false;
    public bool ButtonAdded = false;
    public bool subButtonAdded = false;

    public int connectorStatus = 1;//0 for not, 1 for exp, 2 for inp
    public GameObject inpExpSide;

    //   [SerializeField]
    public int upgradeLevel = 0;

    //box side values//
    public float PurchaseCost = 50f;

    public float UpgradeCost = 150f;

    //   public float IncomeHeld = 0;
    //  public float IncomeRate = 0f;
    public float transferRate = 0f;

    //  public float MaxLimit = 0f;

    //box side values//

    // Use this for initialization
    private void Start()
    {
        //    Debug.Log(transform.parent.gameObject.transform.parent.gameObject.name);
        MainController = transform.parent.gameObject.transform.parent.gameObject;
        CConn = MainController.GetComponent<ConnectorController>();
        thisSide = transform.GetChild(0).gameObject;
        playerCon = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateText();

        if (inpExpSide == null && upgradeLevel != 0)
        {
            // Debug.Log(this.gameObject.name + this.transform.ToString() + "sent to cc trnas");
            //  Debug.Log(this.transform.GetChild(0).GetChild(1).transform.name + this.transform.GetChild(0).GetChild(1).transform.transform.ToString() + "sent to cc trnas");
            //inpExpSide = CConn.GetNearObjects(this.gameObject.transform, true);

            //since dist check is activated when this will be called then it can be used to get the inpexptile

            inpExpSide = CConn.GetNearObjects(this.transform.GetChild(0).GetChild(1).transform, true);
            CConn.UpdateConnObj();
            CConn.UpdateInpExpLists();
        }
    }

    private void UpdateText()
    {
        //updates buttons text using button held above
        if (buttonAssigned != null)
        {
            if (upgradeLevel == 3)
            {
                buttonAssigned.transform.GetChild(0).GetComponent<Text>().text = "MAX";
            }
            else
            {
                buttonAssigned.transform.GetChild(0).GetComponent<Text>().text = upgradeLevel.ToString();
            }
        }
    }

    private void SideConnectorCol()
    {
        Material mat = transform.GetChild(0).GetChild(0).transform.GetComponent<MeshRenderer>().material;
        switch (connectorStatus)
        {
            case 1:

                mat.color = Color.red;

                break;

            case 2:
                mat.color = Color.blue;

                break;
        }
    }

    public void SwapConnType()
    {
        //Debug.Log("swapped connect int");
        switch (connectorStatus)
        {
            case 1: connectorStatus = 2; break;
            case 2: connectorStatus = 1; break;
        }

        // boxSideFocused.GetComponent<BoxSideController>().MainBox.GetComponent<BoxController>().UpdateInpExpLists();

        CConn.UpdateInpExpLists();

        SideConnectorCol();
    }

    public void ModifySide()
    {
        //allow: purchase, upgrade

        if (sidePresent == false)
        {
            //buy side - check money,check stats
            if (playerCon.Money > PurchaseCost)
            {
                //can afford purchase
                sidePresent = true;
                upgradeLevel = 1;
                playerCon.Money -= PurchaseCost;
                thisSide.SetActive(true);
                connectorStatus = 1;

                SideConnectorCol();
            }
            else
            {
                //cant afford purchase

                Debug.Log("cant afford purchase");
            }
        }
        else
        {
            //upgrade side - check money, change stats
            float uCost = 999999f;

            switch (upgradeLevel)
            {
                case 0:
                    Debug.Log("should not reach here : switch - 0");
                    break;

                case 1:
                    uCost = UpgradeCost * upgradeLevel;
                    break;

                case 2:
                    uCost = (UpgradeCost * upgradeLevel) + 25f;
                    break;

                case 3:
                    uCost = (UpgradeCost * upgradeLevel) + 75f;
                    break;

                default: Debug.Log("should not reach here : switch - default"); break;
            }

            if (playerCon.Money > uCost && upgradeLevel < 3)
            {
                //can afford purchase

                //upgrade the side
                playerCon.Money -= uCost;
                upgradeLevel++;

                if (CConn.transferRate < (25 * upgradeLevel)) CConn.transferRate = (25 * upgradeLevel);

                // UpdateRate();
            }
            else
            {
                //cant afford purchase or cant upgrade further
                Debug.Log("cant afford upgrade" + uCost);
            }
        }
    }
}