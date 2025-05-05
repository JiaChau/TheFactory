using UnityEngine;
using UnityEngine.Rendering;

public class CraftingMenu : MonoBehaviour
{
    public GameObject craftingSlotPrefab;
    public Transform craftingSlotContainer;

    public CraftableObjectDataBase craftingDatabase;
    public MachineInventory machineInventory;

    

    public void GenerateCraftingList()
    {
        ClearList();
        foreach (var craftable in craftingDatabase.craftables)
        {
            if (craftable == null)
            {
                Debug.LogError("CraftableData is null in the craftables list.");
                continue;
            }
            GameObject slotGO = Instantiate(craftingSlotPrefab, craftingSlotContainer);
            CraftingSlot slot = slotGO.GetComponent<CraftingSlot>();
            Debug.Log($"Setting up CraftingSlot with CraftableData: {craftable.name}");
            slot.Setup(craftable, machineInventory);
            
        }
    }

    void ClearList()
    {
        // Clear existing slots
        foreach (Transform child in craftingSlotContainer)
        {  
            GameObject GO = child.gameObject;
            if(GO.GetComponent<CraftingSlot>() != null)
                Destroy(GO);
        }
        foreach (Transform child in MaterialPopUI.Instance.contentContainer)
        {
            GameObject GO = child.gameObject;
            if (GO.GetComponent<MaterialRequirementUI>() != null)
                Destroy(GO);
        }
    }
}

