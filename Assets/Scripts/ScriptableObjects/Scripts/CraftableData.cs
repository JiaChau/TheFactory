using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Craftable")]
public class CraftableData : ItemData
{
    [Header("Tool Properties")]
    public ToolType toolType;
    public ToolTier toolTier;
    public float toolDamage = 25f;

    [Header("World/Visual Prefab")]
    public GameObject prefabCreatedInWorld;

    [Header("Crafting Settings")]
    public List<MaterialRequirement> materialsToCraft = new List<MaterialRequirement>();
    public float timeToCraft;
}

[System.Serializable]
public class MaterialRequirement
{
    public ItemData item;
    public int amount;
}