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
    public InventoryUI inventoryUI;

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

   
    public bool AddItem(ItemData data, int amount = 1)
    {
        bool added = false;

        // STEP 1: Try to stack into existing slot(s)
        foreach (var slot in inventory.slots)
        {
            if (slot.CanStackWith(data) && slot.amount < data.maxStackSize)
            {
                int spaceLeft = data.maxStackSize - slot.amount;
                int toAdd = Mathf.Min(spaceLeft, amount);
                slot.amount += toAdd;
                amount -= toAdd;

                if (amount <= 0)
                {
                    added = true;
                    break;
                }
            }
        }

        // STEP 2: Add new slots if there's leftover
        while (amount > 0 && inventory.slots.Count < inventory.maxSlots)
        {
            int toAdd = Mathf.Min(data.maxStackSize, amount);
            inventory.slots.Add(new InventorySlot(data, toAdd));
            amount -= toAdd;
            added = true;
        }

        // STEP 3: Refresh UI if successful
        if (added)
        {
            inventoryUI.RefreshInventoryUI();
        }

        return added;
    }

    //we call other functions, but having here is cleaner
    public void MoveItem(int from, int to)
    {
        inventory.MoveItem(from, to);
        inventoryUI.RefreshInventoryUI();
    }

    // safe backward iteration for removal
    public void ConsumeItem(ItemData data, int amount)
    {
        for (int i = inventory.slots.Count - 1; i >= 0; i--) 
        {
            var slot = inventory.slots[i];
            if (slot.CanStackWith(data))
            {
                if (slot.amount <= amount)
                {
                    amount -= slot.amount;
                    inventory.slots.RemoveAt(i);
                }
                else
                {
                    slot.amount -= amount;
                    amount = 0;
                }

                if (amount <= 0)
                    break;
            }
        }
        //refresh for updated visuals
        inventoryUI.RefreshInventoryUI();
    }

}

