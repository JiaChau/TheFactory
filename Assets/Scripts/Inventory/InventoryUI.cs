///<summary>
///This goes on the parent for the Grid for main inventory "box"
///It handles refreshing as needed to makes sure all data is correct visually
///</summary>
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
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }

    //we use this to make sure that any time it is changed
    //in any way, it stays updated
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
                dragItem.image.sprite = slotData.itemData.icon;
                dragItem.parentAfterDrag = slotGO.transform;

                //amount in inventory
                if (dragItem.amountText != null)
                    dragItem.amountText.text = dragItem.amount > 1 ? dragItem.amount.ToString() : "";
            }
        }
    }
}
