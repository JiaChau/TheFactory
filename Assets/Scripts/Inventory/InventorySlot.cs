///<summary>
///This is a helper class (NOT MONO, NOT ON AN OBJECT)
///That gives us some data for the inventory slots themselves
///</summary>
[System.Serializable]
public class InventorySlot
{
    //item inside slot
    public ItemData itemData;
    //amount inside it
    public int amount;

    public InventorySlot(ItemData data, int amt = 1)
    {
        itemData = data;
        amount = amt;
    }

    public bool CanStackWith(ItemData other)
    {
        return itemData != null && itemData == other;
    }

    public void AddAmount(int amt)
    {
        amount += amt;
    }
}
