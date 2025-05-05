using UnityEngine;

public enum ToolType { Axe, Pickaxe, Hoe }
public enum ToolTier { Basic, Stone, Iron, Gold }

public class Tool : MonoBehaviour
{
    public ToolType toolType;
    public ToolTier toolTier = ToolTier.Basic;
    public float damage = 25f;
}
