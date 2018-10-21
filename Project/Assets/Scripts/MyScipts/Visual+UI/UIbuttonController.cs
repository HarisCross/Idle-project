using UnityEngine;
using UnityEngine.UI;

public class UIbuttonController : MonoBehaviour
{
    private UIInteraction UI;

    public int switchButton = 0;

    // Use this for initialization
    private void Start()
    {
        UI = GameObject.Find("UI").GetComponent<UIInteraction>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (UI.menuDisplayed == true)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
    }
}