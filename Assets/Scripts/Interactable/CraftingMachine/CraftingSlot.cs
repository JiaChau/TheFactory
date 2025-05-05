using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon;
    public Text titleText;
    public GameObject materialRequirementContainer;
    public GameObject materialRequirementPrefab;
    private CraftableData craftableData;
    public bool canCraft;

    public void Setup(CraftableData data, MachineInventory inventory)
    {

        if (materialRequirementContainer == null)
        {
            Debug.LogWarning("materialRequirementContainer is not assigned. Attempting to fetch from CanvasManager.");
            //  Debug.Log("Current canvas type: " + CanvasManagers.Instance.canvasType);
            CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.MaterialPopup);
            materialRequirementContainer = CanvasManagers.Instance.GetSecondaryCanvas().GetComponentInChildren<Image>().gameObject;

            if (materialRequirementContainer == null)
            {
                Debug.LogError("materialRequirementContainer is still null. Cannot proceed.");
                return;
            }
            else
            {
                Debug.Log("materialRequirementContainer successfully assigned.");
            }
        }
        if (materialRequirementPrefab == null)
        {
            Debug.LogWarning("MaterialRequirementPrefab is not assigned. Attempting to fetch from CraftableData.");
            materialRequirementPrefab = data.prefabCreatedForInventory; // This must not be null
            if (materialRequirementPrefab == null)
            {
                Debug.LogError("MaterialRequirementPrefab is still null. Cannot proceed.");
                return;
            }
            else
            {
                Debug.Log("materialRequirementPrefab successfully assigned.");
            }
        }
        if (data == null)
        {
            Debug.LogError("CraftableData is null. Cannot set up CraftingSlot.");
            return;
        }

        if (inventory == null)
        {
            Debug.LogError("Inventory is null. Cannot set up CraftingSlot.");
            return;
        }
        if (!(data is CraftableData)) // Fix for CS8121: Correctly check if 'data' is not of type 'CraftableData'
        {
            Debug.LogError("Provided data is not of type CraftableData.");
            return;
        }

        craftableData = data; // Assign the data after the type check
        //playerInventory = inventory;
        canCraft = CanCraft();

        if (data == null)
        {
            Debug.LogError("CraftableData is null. Cannot set up CraftingSlot.");
            return;
        }

        if (data.icon == null || string.IsNullOrEmpty(data.name))
        {
            Debug.LogError($"CraftableData is missing required properties. Name: {data.name}, Icon: {data.icon}");
            return;
        }

        icon.sprite = data.icon;
        //titleText.text = data.name;
        foreach (var requirement in data.materialsToCraft)
        {
            ItemData item = requirement.item;
            int requiredAmount = requirement.amount;

            GameObject matGO = Instantiate(materialRequirementPrefab, materialRequirementContainer.transform);
            if (matGO.TryGetComponent<MaterialRequirementUI>(out var matUI))
            {
                bool isAvailable = MachineInventoryManager.Instance != null && MachineInventoryManager.Instance.HasItem(item, requiredAmount);
                matUI.Setup(item, requiredAmount, isAvailable);
            }
            else
            {
                Debug.LogError("MaterialRequirementPrefab does not have a MaterialRequirementUI component.");
            }
        }
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        MaterialPopUI.Instance.ShowPopup(craftableData, canCraft);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MaterialPopUI.Instance.HidePopup();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canCraft && !MaterialPopUI.Instance.crafting)
            MachineInventoryManager.Instance.Craft(craftableData);
    }

    

    bool CanCraft()
    {
        foreach (var requirement in craftableData.materialsToCraft)
        {
            if (!MachineInventoryManager.Instance.HasItem(requirement.item, requirement.amount))
            {
                return false;
            }
        }
        return true;

    }
}
