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

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;

            // Optional: update inventory logic here if you're swapping
            inventory.MoveItem(draggableItem.slotIndex, slotIndex);
        }
    }
}