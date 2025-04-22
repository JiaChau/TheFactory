///<summary>
///This was my first attempt at a modular approach I'm 75% I don't need
///this class but I want to triple check before deleting it
///</summary>
using UnityEngine;

public class Item
{
    public Mesh[] meshes;
    
    public enum ItemType
    {
        wood_log,
        cooper
    }
    
    public ItemType itemType;
    public int amount;

    public Mesh GetMesh()
    {
        switch (itemType)
        {
            case ItemType.wood_log:
                return meshes[0];
            case ItemType.cooper:
                return meshes[1];
            default:
                break;
        }
        return null;
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
}
