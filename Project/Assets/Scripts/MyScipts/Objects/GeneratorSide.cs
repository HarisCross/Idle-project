using UnityEngine;
using UnityEngine.UI;

public class GeneratorSide : MonoBehaviour
{
    public string side = "Connector";

    [SerializeField]
    private GeneratorController GenController;

    private GameObject MainController;
    private PlayerController playerCon;
    public GameObject buttonAssigned;
    private GameObject thisSide;
    //public bool sidePresent = false;
    //  public bool ButtonAdded = false;
    //  public bool subButtonAdded = false;

    public int connectorStatus = 2;//0 for not, 1 for exp, 2 for inp
    public GameObject inpExpSide;

    public int upgradeLevel = 0;

    public float PurchaseCost = 50f;
    public float UpgradeCost = 150f;

    public float transferRate = 50f;

    // Use this for initialization
    private void Start()
    {
        MainController = transform.parent.gameObject.transform.parent.gameObject;
        GenController = MainController.GetComponent<GeneratorController>();
        thisSide = transform.GetChild(0).gameObject;
        playerCon = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateText();

        if (inpExpSide == null && upgradeLevel != 0)
        {
            //since dist check is activated when this will be called then it can be used to get the inpexptile

            inpExpSide = GenController.GetNearObjects(this.transform.GetChild(0).GetChild(1).transform, true);
            GenController.UpdateConnObj();
            GenController.UpdateInpExpLists();
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
}