using UnityEngine;

public class PriceText : MonoBehaviour
{
    private int price;

    private TextMesh text;
    private BoardSpaceController parentBoardSpaceController;

    private GameObject player;

    // Use this for initialization
    private void Start()
    {
        text = GetComponent<TextMesh>();
        parentBoardSpaceController = gameObject.GetComponentInParent<BoardSpaceController>();
        price = parentBoardSpaceController.SpaceCost;
        text.text = "£:" + price;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        // transform.LookAt(player.transform);
    }

    private void LateUpdate()
    {
        transform.LookAt((transform.position + player.transform.rotation * Vector3.forward), (player.transform.rotation * Vector3.up));
    }
}