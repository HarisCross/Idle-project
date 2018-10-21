using UnityEngine;

public class BoxSideDetails : MonoBehaviour
{
    public string side = "Side";
    public float IncomeHeld = 0;
    public float IncomeRate = 0f;

    public GameObject BoxParent;
    private Vector3 dirAwayFromBox;

    // Use this for initialization
    private void Start()
    {
        // BoxParent = transform.parent.transform.parent.transform.parent.transform.parent.gameObject;
        //  dirAwayFromBox = transform.position - BoxParent.transform.position;

        //   transform.Rotate(dirAwayFromBox);
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.DrawLine(this.transform.position,)
        //GameObject sideAdded = sidePassed.transform.parent.gameObject;
        //Vector3 dir = sideAdded.transform.position - boxParent.transform.position;
        //print("parent is: " + boxParent);

        //sideAdded.transform.LookAt(dir);

        // Debug.DrawRay(this.transform.position, Vector3.forward);

        // Debug.DrawRay(transform.position, dirAwayFromBox);
    }
}