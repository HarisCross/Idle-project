using System.Collections.Generic;
using UnityEngine;

public class RecieverController : MonoBehaviour
{
    private PlayerController player;

    public List<GameObject> BoxesToImport = new List<GameObject>();

    public List<GameObject> RecievingTiles;

    public float IncomeHeld = 0;
    public float transferRate = 2.5f;
    public float MaxLimit = 1000f;

    // Use this for initialization
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateBTI();

        if (IncomeHeld > MaxLimit)
        {
            IncomeHeld = MaxLimit;
        }
    }

    public void Timedpdate()
    {
        if (IncomeHeld > transferRate)
        {
            //if held is more than transfer rate

            player.AddIncome(transferRate);
            IncomeHeld -= transferRate;
        }
        else
        {
            //if held isnt more than rate

            player.AddIncome(IncomeHeld);
            IncomeHeld -= IncomeHeld;
        }
    }

    private void UpdateBTI()
    {
        BoxesToImport.Clear();

        foreach (GameObject Tile in RecievingTiles)
        {
            if (Tile.GetComponent<BoardSpaceController>().CurrentBox != null)
            {
                BoxesToImport.Add(Tile.GetComponent<BoardSpaceController>().CurrentBox);
            }
            else
            {
                // print("its null");
                //   print("tile: " + Tile.name + " is null");
            }
        }
    }
}