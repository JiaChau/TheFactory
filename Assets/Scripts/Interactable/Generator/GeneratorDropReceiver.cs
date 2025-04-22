//This script goes on the slot that the item gets dropped on
//Makes sure the drop is compatable
using UnityEngine;

public class GeneratorDropReceiver : MonoBehaviour, IDropReceiver
{
    //essentially this transform is the "slot" or image itself 
    //optimized later
    public Transform fuelSlot;
    //template prfab of the imageItemForInventory
    public GameObject itemUIPrefab;

    public void HandleItemDrop(ItemData itemData, int amount)
    {

       // if item has the fuel property boolean (inside scriptable object), we will continue the logic
       if(itemData.fuelProperties.isFuel)
        {
            Debug.Log("🔥 Fire started with " + itemData.itemType + " x" + amount);

            // 1. Fully consume the item stack from the player inventory
            InventoryManager.Instance.ConsumeItem(itemData, amount);

            // 2. Instantiate new UI item in the fuel slot
            //and set all of the data to new item ("moving" it to new inventory)
            GameObject itemUI = Instantiate(itemUIPrefab, fuelSlot);
            var dragItem = itemUI.GetComponent<DraggableItem>();
            dragItem.itemData = itemData;
            dragItem.amount = amount;
            dragItem.image.sprite = itemData.icon;
            dragItem.parentAfterDrag = fuelSlot;

            if (dragItem.amountText != null)
                dragItem.amountText.text = amount > 1 ? amount.ToString() : "";

            itemUI.transform.localPosition = Vector3.zero;

            
            
        }
        //simply a hanging else, can be taken out later after testing
        else
        {
            Debug.Log("❌ " + itemData.itemType + " can't be used here.");

        }
        // Refresh original inventory UI causing it to snap back to the old UI if it is not a fuel type
        InventoryManager.Instance.inventoryUI.RefreshInventoryUI();
    }
}
