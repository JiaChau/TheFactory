using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craftable Object Database", menuName = "Inventory/DataBases/Craftable Object Database")]
public class CraftableObjectDataBase : ScriptableObject
{
    public List<CraftableData> craftables;
}
