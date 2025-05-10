using TMPro;
using UnityEngine;

public class MaterialPopUI : MonoBehaviour
{
    public static MaterialPopUI Instance;

    public GameObject popupPanel;
    public GameObject materialRequirementPrefab;
    public Transform contentContainer;
    //public Slider craftingTimer;
    public TMP_Text text;
    public bool crafting;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            print("INSTANCE of " + gameObject.name + " Was null and deleted one");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            popupPanel.SetActive(false);
            text.text = "";
           // TurnOffSlider();
        }

    }

    public void ShowPopup(CraftableData data, bool readyToCraft)
    {
        text.enabled = true;
        popupPanel.SetActive(true);

        // Clear previous content
        foreach (Transform child in contentContainer)
        {
            GameObject GO = child.gameObject;
            if (GO.GetComponent<MaterialRequirementUI>() != null)
                Destroy(GO);
        }
        foreach(var requirement in data.materialsToCraft)
        {
            GameObject matGO = Instantiate(materialRequirementPrefab, contentContainer);
            var matUI = matGO.GetComponent<MaterialRequirementUI>();
            bool isAvailable = MachineInventoryManager.Instance.HasItem(requirement.item, requirement.amount);
            matUI.Setup(requirement.item, requirement.amount, isAvailable);
        }
        // Update the crafting message
        if (!crafting)
            text.text = readyToCraft ? "Press LMB TO Craft" : "NOT ENOUGH";
        else
            text.text = "Currently Crafting";
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
        text.text = "";
        text.enabled = false;

        CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.CraftingMachine);
    }

    //public void TurnOnSlider()
    //{
    //    craftingTimer.gameObject.SetActive(true);

    //}
    //public void TurnOffSlider()
    //{
    //    craftingTimer.gameObject.SetActive(false);

    //}
    //public void UpdateSlider(float val, float val2)
    //{
    //    craftingTimer.value = val / val2;

    //}

}
