using System.Collections.Generic;
using UnityEngine;

public class MachineInventory : MonoBehaviour
{

    public CraftingMenu craftingMenu;
    public List<InventorySlot> slots = new();
    public int maxSlots = 12;
    public Transform spawnLocation;
   

    private void Awake()
    {
        //prefill inventory with empty slots
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null, 0, i));
        }
        MachineUI.Instance?.RefreshMachineUI();
    }

    public bool AddItem(ItemData data, int amount = 1)
    {
        bool added = false;
        // Loop through each slot in the inventory
        foreach (var slot in slots)
        {
            // Check if the slot already has the same item type and isn't full
            if (slot.CanStackWith(data) && slot.amount < data.maxStackSize)
            {
                // Calculate how much more can fit in the slot
                int spaceLeft = data.maxStackSize - slot.amount;
                // Determine how many items we can add (can't exceed what's left or what we have)
                int toAdd = Mathf.Min(spaceLeft, amount);
                // Add items to the slot
                slot.amount += toAdd;
                // Subtract the added amount from the total remaining to add
                amount -= toAdd;
                // Flag that at least some of the item was added
                added = true;
                //if (selectedSlot == null)
                //    selectedSlot = slot;
                SelectSlot(0);

                // If there's nothing left to add, stop the loop early
                if (amount <= 0)
                    break;
            }
            
        }

        foreach (var slot in slots)
        {
            // Stop if we've already added all the items
            if (amount <= 0)
                break;
            // Check if the slot is completely empty
            if (slot.IsEmpty)
            {
                // Add as much as we can, up to the max stack size
                int toAdd = Mathf.Min(data.maxStackSize, amount);
                // Assign the item type and amount to the empty slot
                slot.itemData = data;
                slot.amount = toAdd;
                // Subtract what we just added
                amount -= toAdd;
                // Mark that we successfully added something
                added = true;
                SelectSlot(0);
            }
        }

        while (amount > 0 && slots.Count < maxSlots)
        {
            


            // Add a new slot with as much as we can (up to max stack size)
            int toAdd = Mathf.Min(data.maxStackSize, amount);
            // Create and add the new slot with the item and amount
            var newSlot = new InventorySlot(data, toAdd, slots.Count);
            slots.Add(newSlot);
            // Subtract what we just added
            amount -= toAdd;
            // Flag as added
            added = true;
            // Select first added slot if none selected yet
            // if (selectedSlot == null)

            SelectSlot(0);

        }
        if (added)
            //RefreshUI
            MachineUI.Instance.RefreshMachineUI();
            craftingMenu.GenerateCraftingList();

        return added;
    }
    public void MoveItem(int fromIndex, int toIndex)
    {
        // Ensure the fromIndex and toIndex are within valid range of the slot list
        if (fromIndex < 0 || fromIndex >= slots.Count || toIndex < 0 || toIndex >= slots.Count)
            return;
        // Get references to the source and destination inventory slots
        InventorySlot fromSlot = slots[fromIndex];
        InventorySlot toSlot = slots[toIndex];
        // If both slots contain the same item type, try to stack them
        if (fromSlot.itemData == toSlot.itemData)
        {
            // Calculate how many items we can move without exceeding the max stack size
            int availableSpace = toSlot.itemData.maxStackSize - toSlot.amount;
            // Move only as much as we can fit
            int toMove = Mathf.Min(availableSpace, fromSlot.amount);
            // Add to the destination slot and subtract from the source
            toSlot.amount += toMove;
            fromSlot.amount -= toMove;
            // If the source slot is now empty, clear it
            if (fromSlot.amount <= 0)
                fromSlot.Clear();
        }
        else
        {
            // If the slots contain different items, swap their contents
            InventorySlot temp = new InventorySlot(fromSlot.itemData, fromSlot.amount, fromSlot.slotIndex);
            // Copy 'toSlot' data into 'fromSlot'
            fromSlot.itemData = toSlot.itemData;
            fromSlot.amount = toSlot.amount;
            // Copy stored temp data into 'toSlot'
            toSlot.itemData = temp.itemData;
            toSlot.amount = temp.amount;
        }
        //RefreshUI;
        MachineUI.Instance.RefreshMachineUI();              
    }
    public void RemoveItem(ItemData item, int amountToRemove)
    {
        foreach (var slot in slots)
        {
            if (slot.itemData == item)
            {
                slot.amount -= amountToRemove;

                // Optionally, you can check to remove the slot entirely if the amount goes to 0
                if (slot.amount <= 0)
                {
                    slot.Clear();
                }
                break;  // Exit loop after modifying the item
            }
        }
        MachineUI.Instance.RefreshMachineUI();
    }

    public void ConsumeItem(ItemData data, int amount)
    {
        // Iterate through the inventory slots in reverse order (from last to first)
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            var slot = slots[i];
            // Check if this slot contains an item that can stack with the given item data
            if (slot.CanStackWith(data))
            {
                // If the slot has less than or equal to the amount we want to remove
                if (slot.amount <= amount)
                {
                    // Subtract the entire slot amount from the amount we still need to remove
                    amount -= slot.amount;
                    // Clear the slot since it's now empty
                    slot.Clear();
                    //InventoryUI.Instance.RefreshInventoryUI();
                }
                else
                {
                    // If the slot has more than enough, just subtract the needed amount
                    slot.amount -= amount;
                    // All required amount has been removed
                    amount = 0;
                }

                // If there's no more amount to remove, stop looping
                if (amount <= 0)
                    break;
            }
        }
        MachineUI.Instance.RefreshMachineUI();
    }
    public void SelectSlot(int index)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i == index)
            {
                slots[i].isSelected = true;
            }
            else
            {
                slots[i].isSelected = false;
            }
        }
    }
   
    

    public bool IsInventoryEmpty()
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty)
            {
                return false;
            }
        }
        return true;
    }

    public InventorySlot GetSelectedItem()
    {
        foreach (var slot in slots)
        {
            if (slot.isSelected)
            {
                return slot;
            }
        }
        return null;
    }

    public void SpawnCraftedItem(CraftableData craftableData)
    {
        Instantiate(craftableData.prefabCreatedInWorld, spawnLocation);
    }
}


