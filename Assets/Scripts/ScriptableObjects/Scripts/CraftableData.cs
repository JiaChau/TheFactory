using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
[System.Serializable]
public class MaterialRequirement
{
    public ItemData item;
    public int amount;
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Craftable")]
public class CraftableData : ItemData
{
    public GameObject prefabCreatedInWorld;
    public List<MaterialRequirement> materialsToCraft = new List<MaterialRequirement>();
    public float timeToCraft;
}
