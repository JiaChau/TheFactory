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
        wood_log,
        cooper,
        coal,
        BasicAxe,
        StoneAxe,
        IronAxe,
        GoldAxe
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
}
