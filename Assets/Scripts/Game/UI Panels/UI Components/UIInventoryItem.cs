using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public Image image;

    public void SetAmount(int itemAmount)
    {
        amount.SetText(itemAmount.ToString());
    }

    public void SetImage(Image itemImage)
    {
        image.sprite = itemImage.sprite;
    }
}