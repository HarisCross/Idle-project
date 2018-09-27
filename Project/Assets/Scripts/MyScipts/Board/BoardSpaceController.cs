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


    public bool openSpace = true;
    public int SpaceCost = 50;

	// Use this for initialization
	void Start () {

        AllocateNumbers();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AllocateNumbers()
    {

        string name = gameObject.name;

        char xChar = name[0];
        char yChar = name[1];

        // x = int.Parse(name[]);

        x = (int)char.GetNumericValue(xChar);
        y = (int)char.GetNumericValue(yChar);


    }
}
