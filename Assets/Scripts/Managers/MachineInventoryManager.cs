using System.Collections;
using UnityEngine;

public class MachineInventoryManager : MonoBehaviour
{
    public static MachineInventoryManager Instance;
    public MachineInventory inventory;
    public GeneratorFuelBurner generator;

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

    void CraftItem(CraftableData craftableData)
    {
            foreach (var requirement in craftableData.materialsToCraft)
            {
                RemoveItem(requirement.item, requirement.amount);
            }
            //these can be interchanged depending on if we want it to go directly into the machines inventory
            // AddItem(craftableData, 1);
            //or spawned into the world
            SpawnCraftedItem(craftableData);       
    }
    //this is moved to the singleton
    public IEnumerator StartCrafting(CraftableData craftableData)
    {
        MaterialPopUI.Instance.crafting = true;
        float timer = craftableData.timeToCraft;
        //this can be brough back if we want to show a crafting timer
        // MaterialPopUI.Instance.TurnOnSlider();
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            //  MaterialPopUI.Instance.UpdateSlider(timer, craftableData.timeToCraft);
            yield return null;
            print("Crafting with " + timer + " Time Left");
        }
        CraftItem(craftableData);
        MaterialPopUI.Instance.crafting = false;
        // MaterialPopUI.Instance.TurnOffSlider();
    }
    public void SpawnCraftedItem(CraftableData craftedItem)
    {
        inventory.SpawnCraftedItem(craftedItem);
    }

    public void Craft(CraftableData craftableData)
    {
        //only craft id generator is on
        if(generator.GetStateOfGenerator())
            StartCoroutine(StartCrafting(craftableData));
    }
}
