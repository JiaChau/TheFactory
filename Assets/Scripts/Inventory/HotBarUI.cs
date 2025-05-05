using UnityEngine;

public class HotBarUI : MonoBehaviour
{
    public static HotBarUI Instance;
    public Inventory playerInventory;
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    public Transform hotbarSlotContainer;
    private void Awake()
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

    public void RefreshHotbarUI()
    {
        //  Debug.Log("Refreshing Hotbar UI");

        // Clear current hotbar slot UI
        foreach (Transform child in hotbarSlotContainer)
        {
            Destroy(child.gameObject);
        }

        // Loop through each hotbar slot and create its UI
        for (int i = 0; i < playerInventory.hotBarSlots.Count; i++)
        {
            var slotGO = Instantiate(slotPrefab, hotbarSlotContainer);
            var slotUI = slotGO.GetComponent<InventorySlotUI>();
            slotUI.slotIndex = i;
            slotUI.isHotbarSlot = true;
            slotUI.inventory = playerInventory;

            InventorySlot slotData = playerInventory.hotBarSlots[i];

            if (slotData.itemData != null)
            {
                var itemGO = Instantiate(itemPrefab, slotGO.transform);
                var dragItem = itemGO.GetComponent<DraggableItem>();

                dragItem.itemData = slotData.itemData;
                dragItem.amount = slotData.amount;
                dragItem.image.sprite = slotData.itemData.icon;
                dragItem.parentAfterDrag = slotGO.transform;
                dragItem.slotIndex = i;
                // dragItem.sourceInventory = DraggableItem.InventoryType.HotBar;
                if (dragItem.amountText != null)
                    dragItem.amountText.text = dragItem.amount > 1 ? dragItem.amount.ToString() : "";
            }
        }
    }

}
