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
