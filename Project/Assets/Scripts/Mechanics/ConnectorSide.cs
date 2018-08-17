using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorSide : MonoBehaviour {

    public string side = "Connector";
    [SerializeField]
    private ConnectorController CBConn;

    public bool sidePresent = false;
    public bool ButtonAdded = false;

    //box side values//

    public float IncomeHeld = 0;
    public float IncomeRate = 0f;
    public float transferRate = 0f;
    public float MaxLimit = 0f;

    //box side values//

    // Use this for initialization
    void Start () {
    //    Debug.Log(transform.parent.gameObject.transform.parent.gameObject.name);
        CBConn = transform.parent.gameObject.transform.parent.gameObject.GetComponent<ConnectorController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ModifySide()
    {
        Debug.Log("test: " + this.name + this.transform.position);
    }
}
