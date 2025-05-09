using UnityEngine;

public enum ToolType { Axe, Pickaxe, Hoe }
public enum ToolTier { Basic, Stone, Iron, Gold }

public class Tool : MonoBehaviour
{
    [SerializeField] private ToolType toolType;
    [SerializeField] private ToolTier toolTier = ToolTier.Basic;
    [SerializeField] private float damage = 25f;

    // Public properties to access the private fields
    public ToolType ToolType => toolType;
    public ToolTier ToolTier => toolTier;
    public float Damage => damage;

    // Initialize the tool's properties from the provided CraftableData
    public void Initialize(CraftableData data)
    {
        toolType = data.toolType;
        toolTier = data.toolTier;
        damage = data.toolDamage;
    }
}
