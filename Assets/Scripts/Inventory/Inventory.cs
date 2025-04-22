///<summary>
///The actual inventory several dumbed down
///only handles the visual slots now. 
///currently: no slots visible and slots get added dynamically as needed
///MIGHT CHANGE so player can organize inventory
///</summary>

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 24;

    //handles the adding of the visual slots
    public bool AddItem(ItemData data, int amount = 1)
    {
        if (slots.Count < maxSlots)
        {
            slots.Add(new InventorySlot(data, amount));
            return true;
        }

        return false;
    }

    //Simple swapping function
public void MoveItem(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || fromIndex >= slots.Count || toIndex < 0 || toIndex >= slots.Count)
            return;

        InventorySlot temp = slots[toIndex];
        slots[toIndex] = slots[fromIndex];
        slots[fromIndex] = temp;

        // After swapping, refresh UI to reflect the move
        InventoryUI.Instance.RefreshInventoryUI();
    }


}
