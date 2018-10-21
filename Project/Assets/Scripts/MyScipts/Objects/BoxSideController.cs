using UnityEngine;

public class BoxSideController : MonoBehaviour
{
    public BoxController MainBox;
    public float rotateAmount;
    public bool boxSidePresent = false;
    public bool Rec = false;

    public string side = "";
    public float IncomeHeld = 0;
    public float IncomeRate = 0f;
    public float transferRate = 2.5f;
    public float MaxLimit = 150f;

    public int connectorStatus = 0;//0 for not, 1 for exp, 2 for inp
    public GameObject inpExpSide;
    //box side values//

    public int SideNumber;

    // Use this for initialization
    private void Start()
    {
    }

    public void Timedpdate()
    {
        IncomeHeld += IncomeRate;
        // Debug.Log("box side timed");

        //add to main box
        //take from incomeheld
        if (IncomeHeld > transferRate)
        {
            //if held is more than transfer rate

            MainBox.AddIncome(transferRate);
            IncomeHeld -= transferRate;
        }
        else
        {
            //if held isnt more than rate

            MainBox.AddIncome(IncomeHeld);
            IncomeHeld -= IncomeHeld;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Rec == false)
        {
            if (MainBox == null)
            {
                Collider[] col = Physics.OverlapSphere(this.transform.position, 1f);
                int i = 0;
                while (i < col.Length)
                {
                    if (col[i].transform.gameObject.tag == "MainBox")
                    {
                        MainBox = col[i].transform.gameObject.GetComponent<BoxController>();
                        MainBox.boxSides.Add(this.transform.gameObject);
                    }
                }
            }
            else
            {
                //Vector3 dir = this.transform.position - MainBox.transform.position;

                //// Debug.DrawLine(this.transform.position, MainBox.transform.position);
                //Debug.DrawRay(this.transform.position, dir);
                ////Debug.DrawLine(this.transform.position, GameObject.Find("Player").transform.position);
            }

            if (side == "Connector" & inpExpSide == null)
            {
                inpExpSide = MainBox.GetNearObjects(this.gameObject.transform, true);
                MainBox.UpdateConnObj();
            }

            TEMPSideConnectorCol();

            if (IncomeHeld > MaxLimit)
            {
                IncomeHeld = MaxLimit;
            }
        }
    }

    private void TEMPSideConnectorCol()
    {
        if (connectorStatus != 0)
        {
            if (transform.childCount < 2) return;
            Material mat = transform.GetChild(2).transform.GetChild(0).transform.GetComponent<MeshRenderer>().material;

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
    }
}