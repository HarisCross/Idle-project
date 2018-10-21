using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public double Money = 1000;
    public double Feed = 0;
    public Text text_Money;
    private string string_money;

    // Use this for initialization
    private void Start()
    {
        string_money = "Funds:\n " + Money.ToString() + "\nFeed:\n " + Feed.ToString(); ;
    }

    // Update is called once per frame
    private void Update()
    {
        text_Money.text = string_money;
    }

    public void AddIncome(float amount)
    {
        //   Debug.Log("incomed added to: " + this.name);
        double INTAmount = Mathf.RoundToInt(amount);
        Money += INTAmount;
        string_money = "Funds:\n " + Money.ToString() + "\nFeed:\n " + Feed.ToString(); ;
    }

    public void AddFeed(float amount)
    {
        double INTAmount = Mathf.RoundToInt(amount);
        Feed += INTAmount;
        string_money = "Funds:\n " + Money.ToString() + "\nFeed:\n " + Feed.ToString(); ;
    }
}