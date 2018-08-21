using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectorSide : MonoBehaviour {

    public string side = "Connector";
    [SerializeField]
    private ConnectorController CConn;
    private PlayerController playerCon;
    public GameObject buttonAssigned;
    private GameObject thisSide;
    public bool sidePresent = false;
    public bool ButtonAdded = false;

    public int connectorStatus = 1;//0 for not, 1 for exp, 2 for inp
    public GameObject inpExpSide;

    [SerializeField]
    private int upgradeLevel = 0;

    //box side values//
    public float PurchaseCost = 50f;
    public float UpgradeCost = 150f;
 //   public float IncomeHeld = 0;
  //  public float IncomeRate = 0f;
     public float transferRate = 0f;
  //  public float MaxLimit = 0f;

    //box side values//

    // Use this for initialization
    void Start () {
    //    Debug.Log(transform.parent.gameObject.transform.parent.gameObject.name);
        CConn = transform.parent.gameObject.transform.parent.gameObject.GetComponent<ConnectorController>();
        thisSide = transform.GetChild(0).gameObject;
        playerCon = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateText();
        SideConnectorCol();


    }

    void UpdateText()
    {
        //updates buttons text using button held above

        if(upgradeLevel == 3)
        {
            buttonAssigned.transform.GetChild(0).GetComponent<Text>().text = "MAX";
        }
        else
        {
            buttonAssigned.transform.GetChild(0).GetComponent<Text>().text = upgradeLevel.ToString();
        }

       


    }
    void SideConnectorCol()
    {
        //if (connectorStatus != 0)
        //{

            //  if (transform.childCount < 2) return;
            //Material mat = transform.GetChild(2).transform.GetChild(0).transform.GetComponent<MeshRenderer>().material;
            Material mat = transform.GetChild(0).GetChild(0).transform.GetComponent<MeshRenderer>().material;
         //   Debug.Log(transform.GetChild(0).GetChild(0).transform.name);
            switch (connectorStatus)
            {
                case 1:

                    mat.color = Color.red;


                    break;
                case 2:
                    mat.color = Color.blue;


                    break;
            }

        //}
    }
    //}
    public void ModifySide()
    {
        //allow: purchase, upgrade

        if(sidePresent == false)
        {
            //buy side - check money,check stats
            if(playerCon.Money > PurchaseCost)
            {
                //can afford purchase
                sidePresent = true;
                upgradeLevel = 1;
                playerCon.Money -= PurchaseCost;
                thisSide.SetActive(true);
                connectorStatus = 1;

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

            if (playerCon.Money > uCost && upgradeLevel <= 3)
            {
                //can afford purchase

                //upgrade the side
                playerCon.Money -= uCost;
                upgradeLevel++;
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
