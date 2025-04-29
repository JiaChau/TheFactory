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
   // public Material material;
    public ItemType itemType;
    public int amount;
    public int maxStackSize = 99;
    public FuelProperties fuelProperties;

    public enum ItemType
    {
        wood_log,
        cooper
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }
}
