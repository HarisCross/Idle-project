using UnityEngine;
using UnityEngine.UI;

public class IconPreview : MonoBehaviour
{
    public Sprite[] icons;
    private GameObject icon;

    // Use this for initialization
    private void Awake()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icon = new GameObject("icon" + i);
            icon.transform.SetParent(this.gameObject.transform);
            icon.AddComponent<RectTransform>();
            icon.AddComponent<Image>();
            icon.GetComponent<Image>().sprite = icons[i];
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}