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