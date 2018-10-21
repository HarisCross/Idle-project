using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnButtonController : MonoBehaviour
{
    public CameraController CCon;
    public bool Active = false;
    public bool ButtonsActive = false;
    public List<GameObject> ConnButtons = new List<GameObject>();

    public GameObject ConnTarget;
    public List<GameObject> ConnSides = new List<GameObject>();

    //conn values//
    ///private float SideCost = 50f;

    //conn values//

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (ConnTarget != null)
        {
            //add its sides to list
            if (ConnSides.Count == 0)
            {
                ConnSides.AddRange(ConnTarget.GetComponent<ConnectorController>().ConnectorSides);
            }
            //      ActivateButtons();
        }
        else
        {
            //clear the liust

            ConnSides.Clear();
            //      DeactivateButtons();
        }
        if (Active == false)
        {
            ConnTarget = null;
            ConnSides.Clear();
        }

        if (Active) MoveUI();
    }

    private void MoveUI()
    {
        //for each side get button and have hover over side pos
        //   Debug.Log("moveuio called");
        int count = 0;

        foreach (GameObject side in ConnSides)
        {
            ConnectorSide CSide = side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>();
            Vector3 sidePos = Camera.main.WorldToScreenPoint(side.transform.position);

            ConnButtons[count].transform.position = sidePos;

            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded == false)
            {
                // ConnectorSide CSide = side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>();
                // button.GetComponent<Button>().onClick.AddListener(CSide.ModifySide);

                ConnButtons[count].gameObject.GetComponent<Button>().onClick.AddListener(CSide.ModifySide);
                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().buttonAssigned = ConnButtons[count];

                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded = true;
            }

            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().upgradeLevel > 0)
            {
                // Debug.Log("> 0");
                if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().subButtonAdded == false)
                {
                    ConnButtons[count].transform.GetChild(1).transform.GetComponent<Button>().onClick.AddListener(CSide.SwapConnType);
                    side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().subButtonAdded = true;
                    // Debug.Log("listener added");
                }

                switch (CSide.connectorStatus)
                {
                    case 1:
                        ConnButtons[count].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Exp";
                        break;

                    case 2:
                        ConnButtons[count].transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Inp";
                        break;

                    default: break;
                }
            }
            count++;
        }
    }

    public void ActivateButtons()
    {
        foreach (GameObject butt in ConnButtons)
        {
            StartCoroutine(FillButton(butt, true));
        }
    }

    public void DeactivateButtons()
    {
        //Debug.Log("deactivate buttons");
        foreach (GameObject butt in ConnButtons)
        {
            // Debug.Log("doing: "+butt.name);
            StartCoroutine(FillButton(butt, false));
        }
        int count = 0;

        foreach (GameObject side in ConnSides)
        {
            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded == true)
            {
                ConnButtons[count].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                ConnButtons[count].transform.GetChild(1).transform.GetComponent<Button>().onClick.RemoveAllListeners();
                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().ButtonAdded = false;
            }

            if (side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().subButtonAdded == true)
            {
                ConnButtons[count].transform.GetChild(1).transform.GetComponent<Button>().onClick.RemoveAllListeners();
                side.transform.parent.transform.gameObject.GetComponent<ConnectorSide>().subButtonAdded = false;
            }

            count++;
        }
    }

    private IEnumerator FillButton(GameObject button, bool var)
    {
        float timer = 1f;
        if (var)
        {
            // CCon.moving = true;
            // yield return new WaitForSecondsRealtime(1f);
            do
            {
                // button.GetComponent<Image>().fillAmount += 0.01f;

                button.GetComponent<Image>().fillAmount += timer * Time.deltaTime;
                button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount += timer * Time.deltaTime;
                yield return new WaitForSecondsRealtime(0.005f);
                //  Debug.Log("filling");
            } while (button.GetComponent<Image>().fillAmount < 0.95f);

            // Debug.Log("stopped filling");
            button.GetComponent<Image>().fillAmount = 1f;
            button.GetComponent<Button>().interactable = true;
            button.transform.GetChild(0).transform.gameObject.SetActive(true);

            //timer = 1f;
            //do
            //{
            //    // button.GetComponent<Image>().fillAmount += timer * Time.deltaTime;
            //    button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount += timer * Time.deltaTime;

            //    yield return new WaitForSecondsRealtime(0.01f);
            //} while (button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount < 0.95f);

            button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount = 1f;
            button.transform.GetChild(1).transform.GetComponent<Button>().interactable = true;
            button.transform.GetChild(1).transform.transform.GetChild(0).transform.gameObject.SetActive(true);

            // yield return new WaitForSecondsRealtime(1f);
            // CCon.moving = false;
        }
        else
        {
            // CCon.moving = true;
            button.GetComponent<Button>().interactable = false;
            button.transform.GetChild(0).transform.gameObject.SetActive(false);
            do
            {
                button.GetComponent<Image>().fillAmount -= timer * Time.deltaTime;
                button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount -= timer * Time.deltaTime;

                yield return new WaitForSecondsRealtime(0.01f);
            } while (button.GetComponent<Image>().fillAmount > 0);
            button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount = 0f;

            button.transform.GetChild(1).transform.GetComponent<Button>().interactable = false;
            button.transform.GetChild(1).transform.transform.GetChild(0).transform.gameObject.SetActive(false);

            //timer = 1f;
            //do
            //{
            //    // button.GetComponent<Image>().fillAmount += timer * Time.deltaTime;
            //    button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount -= timer * Time.deltaTime;

            //    yield return new WaitForSecondsRealtime(0.01f);
            //} while (button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount >0);

            button.transform.GetChild(1).transform.GetComponent<Image>().fillAmount = 0f;

            // yield return new WaitForSecondsRealtime(2f);
            // CCon.moving = false;
        }
        yield return null;
    }
}