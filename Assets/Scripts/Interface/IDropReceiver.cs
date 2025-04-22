///<summary>
///Interface to aid moving Inventory items from one UI canvas to another
///</summary>
public interface IDropReceiver
{
    void HandleItemDrop(ItemData itemData, int amount);
}

