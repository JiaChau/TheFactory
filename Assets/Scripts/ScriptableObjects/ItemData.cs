///<summary>
///Scriptable Object:
///At the top is a helper class that defines whether it is a fuel or not
///Below helps us build the actual item for in game purposes
///</summary>
using UnityEngine;

[System.Serializable]
public class FuelProperties
{
    public bool isFuel;
    public float burnDuration;
}



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public Mesh mesh;
    public Sprite icon;
    //this is taken out for now because all the assets
    //so far use the same material, but that may not be true forever
    public Material material;
    public ItemType itemType;
    public int amount;
    public int maxStackSize = 99;
    public FuelProperties fuelProperties;

    //will expand as more items are created
    public enum ItemType
    {
        wood_log,
        cooper,
        coal
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
}
