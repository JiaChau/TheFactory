using UnityEngine;
using UnityEngine.EventSystems;

public class MachineSlotUI : MonoBehaviour
{
    public int slotIndex; // This links back to the player's inventory list
    public MachineInventory machineInventory;
    public bool isHotbarSlot;
    public void OnDrop(PointerEventData eventData)
    {
        var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        // var draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggedItem != null && !isHotbarSlot)
        {
            // Move the item between slots based on their indices
            MachineInventoryManager.Instance.MoveItem(draggedItem.slotIndex, this.slotIndex);
            machineInventory.SelectSlot(this.slotIndex);
            MachineUI.Instance.RefreshMachineUI();
        }
    }
}
