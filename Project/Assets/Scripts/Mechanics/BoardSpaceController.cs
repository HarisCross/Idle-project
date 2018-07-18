using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpaceController : MonoBehaviour {

    public int x,y;

    public bool TakenByB = false;
    public bool TakeByS = false;

    public GameObject CurrentBox;

    public List<GameObject> PossibleSides;
    public List<GameObject> PossibleSideOpenSpots;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
