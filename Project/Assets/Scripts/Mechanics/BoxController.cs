using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

   public  List<GameObject> boxSides = new List<GameObject>();

    private bool BoxFocused = false;

    private void Awake()
    {


        if (boxSides.Count == 0)
        {

            Collider[] col = Physics.OverlapSphere(this.transform.position, 9f);
            //Debug.Log("col" + col.Length);
            int i = 0;
            while (i < col.Length)
            {
                if (col[i].transform.gameObject.tag == "BoxSide")
                {
                  //  Debug.Log(Vector3.Distance(this.gameObject.transform.position, col[i].gameObject.transform.position));
                    boxSides.Add(col[i].gameObject);

                }
                i++;
            }

        }



    }
    // Use this for initialization
    void Start () {




	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
