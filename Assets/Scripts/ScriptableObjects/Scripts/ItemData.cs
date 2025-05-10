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
    public Material material;
    public ItemType itemType;
    public int amount;
    public int maxStackSize = 99;
    public FuelProperties fuelProperties;
    public GameObject prefabCreatedForInventory;

    public enum ItemType
    {
        // Resource drops
        wood_log,
        cooper,
        coal,
        rocks,
        iron,
        gold,

        // Axes
        BasicAxe,
        StoneAxe,
        IronAxe,
        GoldAxe,

        // Pickaxes
        BasicPickaxe,
        StonePickaxe,
        IronPickaxe,
        GoldPickaxe,

        // Hoes
        BasicHoe,
        StoneHoe,
        IronHoe,
        GoldHoe
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
}
