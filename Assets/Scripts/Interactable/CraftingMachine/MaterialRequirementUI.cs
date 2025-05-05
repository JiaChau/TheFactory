using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialRequirementUI : MonoBehaviour
{
    public Image icon = null;
    public TMP_Text amountText = null;

    public void Setup(ItemData data, int requiredAmount, bool isAvailable)
    {
        icon.sprite = data.icon;
        amountText.text = requiredAmount.ToString(); // Placeholder, can be updated for quantity
        // Update the alpha based on availability
        Color iconColor = icon.color;
        iconColor.a = isAvailable ? 1f : 0.5f;
        icon.color = iconColor;
    }

}

