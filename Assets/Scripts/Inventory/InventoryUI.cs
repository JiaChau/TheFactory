using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public Inventory playerInventory;
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    public Transform slotContainer;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshInventoryUI()
    {
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerInventory.slots.Count; i++)
        {
            var slotGO = Instantiate(slotPrefab, slotContainer);
            var slotUI = slotGO.GetComponent<InventorySlotUI>();
            slotUI.slotIndex = i;
            slotUI.inventory = playerInventory;

            InventorySlot slotData = playerInventory.slots[i];
            if (slotData.itemData != null)
            {
                var itemGO = Instantiate(itemPrefab, slotGO.transform);
                var dragItem = itemGO.GetComponent<DraggableItem>();

                dragItem.itemData = slotData.itemData;
                dragItem.amount = slotData.amount;
                dragItem.image.sprite = slotData.itemData.icon; // Assuming ItemData has an icon sprite
                dragItem.parentAfterDrag = slotGO.transform;

                //  Set the image/icon
                if (dragItem.image != null && dragItem.itemData.icon != null)
                    dragItem.image.sprite = dragItem.itemData.icon;

                //  Set the amount text (this is the line you asked about!)
                if (dragItem.amountText != null)
                    dragItem.amountText.text = dragItem.amount > 1 ? dragItem.amount.ToString() : "";
            }
        }
    }
}
