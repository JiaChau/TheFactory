///<summary>
///Scriptable Object:
///a Database of Items that allows us to select an individual item from the array and assign it
///to the singluar prefab and pass it's data into the world as needed
///</summary>
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemData[] items;

    public ItemData GetItemByType(ItemData.ItemType type)
    {
        foreach (var item in items)
        {
            if (item.itemType == type)
                return item;
        }
        return null;
    }
}