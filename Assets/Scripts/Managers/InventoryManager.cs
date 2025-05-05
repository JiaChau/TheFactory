///<summary>
///This is the Inventory's Singleton that handles the actual math, if you will
///if the other handles visuals, this does the other work. Keeping things separate makes things scalable.
///This is what actually adds items, moves items, or consumes them
///</summary>
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Inventory inventory;
    // public InventoryUI inventoryUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            InventoryUI.Instance?.RefreshInventoryUI();
        }
        
    }

    // Check if the inventory contains a certain item and if there is enough of it
    public bool HasItem(ItemData item, int requiredAmount)
    {
        // print("Reaching for item");
        foreach (var slot in inventory.slots)
        {
            if (slot.itemData == item && slot.amount >= requiredAmount)
            {
                //   print("HAS ENOUGH");
                return true;  // Item is in the inventory with enough amount
            }
        }
        // print("Doesn't have enough");
        return false;  // Item not found or not enough in the inventory
    }

    // Remove a certain amount of an item from the inventory
    public void RemoveItem(ItemData item, int amountToRemove)
    {
        inventory.RemoveItem(item, amountToRemove);
    }

    public bool AddItem(ItemData data, int amount = 1)
    {
        
        return inventory.AddItem(data, amount);
        
    }



    //we call other functions, but having here is cleaner
    public void MoveItem(int fromIndex, int toIndex)
    {
        inventory.MoveItem(fromIndex, toIndex);
    }

    // safe backward iteration for removal
    public void ConsumeItem(ItemData data, int amount)
    {
        inventory.ConsumeItem(data, amount);
    }

    public int GetSlotByIndex(int index)
    {
        foreach (var slot in inventory.slots)
        {
            if (index == slot.slotIndex)
            {
                return slot.slotIndex;
            }
        }
        return 99;
    }

    public ItemData GetItemByIndex(int index)
    {
        foreach (var slot in inventory.slots)
        {
            if (slot.slotIndex == index)
                return slot.itemData;
        }
        return null;
    }


    //Simple Debug is checked for in canvas manager since it is already polling 
    //update binded to the K key
    public void DebugPrintInventory()
    {
        Debug.Log("=== INVENTORY ===");
        bool allSlotsEmpty = true;
        if (inventory.slots.Count == 0)
        {
            Debug.Log(" EMPTY ");
        }
        else
        {
            foreach (var slot in inventory.slots)
            {
                if (!slot.IsEmpty)
                {
                    allSlotsEmpty = false;
                    break;
                }
            }
            if (!allSlotsEmpty)
            {
                foreach (var slot in inventory.slots)
                {
                    if (!slot.IsEmpty)
                    {
                        //Debug.Log($"{slot.itemData.GetMasterSubtype()} x {slot.amount} x {slot.itemData.weight}");
                    }
                    else
                    {
                        continue;
                        // Debug.Log("[Empty Slot]");
                    }
                }
            }
            else
            {
                Debug.Log(" EMPTY ");
            }
        }

        Debug.Log("=================");
    }
}


//public bool AddItem(ItemData data, int amount = 1)
//{
//    bool added = false;

//    // STEP 1: Try stacking into existing slots
//    foreach (var slot in inventory.slots)
//    {
//        if (slot.CanStackWith(data) && slot.amount < data.maxStackSize)
//        {
//            int spaceLeft = data.maxStackSize - slot.amount;
//            int toAdd = Mathf.Min(spaceLeft, amount);
//            slot.amount += toAdd;
//            amount -= toAdd;

//            added = true;
//            if (amount <= 0)
//                break;
//        }
//    }

//    // STEP 2: Reuse empty slots
//    foreach (var slot in inventory.slots)
//    {
//        if (amount <= 0)
//            break;

//        if (slot.amount == 0)
//        {
//            int toAdd = Mathf.Min(data.maxStackSize, amount);
//            slot.itemData = data;
//            slot.amount = toAdd;
//            amount -= toAdd;
//            added = true;
//        }
//    }

//    // STEP 3: Add new slots if needed and allowed
//    int usedSlotCount = inventory.slots.Count(s => s.amount > 0);
//    int emptySlotCount = inventory.slots.Count(s => s.IsEmpty); // Count empty slots
//    while (amount > 0 && (usedSlotCount + emptySlotCount) < inventory.maxSlots)
//    {
//        // First, check if you can fit the remaining amount into an empty slot
//        if (emptySlotCount > 0)
//        {
//            // Find an empty slot and add the item there
//            var emptySlot = inventory.slots.FirstOrDefault(s => s.IsEmpty);
//            int toAdd = Mathf.Min(data.maxStackSize, amount);
//            emptySlot.itemData = data;  // Assign the item to the empty slot
//            emptySlot.amount = toAdd;
//            amount -= toAdd;
//            added = true;
//        }
//        else
//        {
//            // If no empty slots, add a new slot
//            int toAdd = Mathf.Min(data.maxStackSize, amount);
//            inventory.slots.Add(new InventorySlot(data, toAdd, inventory.slots.Count));
//            amount -= toAdd;
//            usedSlotCount++; // Increment used slot count
//            added = true;
//        }
//    }


//    // STEP 4: Refresh UI if changes were made
//    if (added)
//    {
//        inventoryUI.RefreshInventoryUI();
//    }

//    return added;
//}






