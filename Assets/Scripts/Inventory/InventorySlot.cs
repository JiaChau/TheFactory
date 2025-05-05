
///<summary>
///This is a helper class (NOT MONO, NOT ON AN OBJECT)
///That gives us some data for the inventory slots themselves
///</summary>
[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int amount;
    public int slotIndex;
    public bool isSelected = false;

    public InventorySlot()
    {
        itemData = null;
        amount = 0;
    }

    public InventorySlot(ItemData itemData, int amount, int slotIndex)
    {
        this.itemData = itemData;
        this.amount = amount;
        this.slotIndex = slotIndex;
    }

    public bool IsEmpty => itemData == null || amount <= 0;

    public bool CanStackWith(ItemData data)
    {
        return itemData != null &&
               itemData == data &&
               itemData.maxStackSize > 1;
    }

    public void Clear()
    {
        itemData = null;
        amount = 0;
    }
}
