
//using System.Collections.Generic;
//using Unity.VisualScripting.Antlr3.Runtime.Misc;
//using UnityEngine;

//public class Inventory
//{
//   private List<Item> itemList;

//    public Inventory()
//    {
//        itemList = new List<Item>();
//        Debug.Log(itemList.Count);
//        foreach(var item in itemList)
//        {
//            Debug.Log(item.amount + " " + item.itemType);

//        }
//    }

//    public void AddItem(Item item)
//    {
//        itemList.Add(item);
//    }

//This goes on the player
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 24;

    public bool AddItem(ItemData data, int amount = 1)
    {
        if (slots.Count < maxSlots)
        {
            slots.Add(new InventorySlot(data, amount));
            return true;
        }

        return false;
    }


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
