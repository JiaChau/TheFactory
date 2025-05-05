///<summary>
///This links to the second half of the players inventory logic (the "dropping") part.
///Allows us drop things and have them lock into the desired location
///</summary>
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    public int slotIndex; // This links back to the player's inventory list
    public Inventory inventory;
    public bool isHotbarSlot;
    public void OnDrop(PointerEventData eventData)
    {
        var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        // var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        
        if (draggedItem != null && !isHotbarSlot)
        {
            if (draggedItem.source == DraggableItem.Source.MainInventory)
            {
                // Move the item between slots based on their indices
                InventoryManager.Instance.MoveItem(draggedItem.slotIndex, this.slotIndex);
                inventory.SelectSlot(this.slotIndex);
            }
            else
            {
                InventoryManager.Instance.AddItem(draggedItem.itemData,draggedItem.amount);
                MachineInventoryManager.Instance.RemoveItem(draggedItem.itemData, draggedItem.amount);
            }
                InventoryUI.Instance.RefreshInventoryUI();

        }
        
        //else
        //{
        //    InventoryManager.Instance.inventory.AddItem()
        //}
    }
}