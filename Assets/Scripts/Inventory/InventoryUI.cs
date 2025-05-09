﻿///<summary>
///This goes on the parent for the Grid for main inventory "box"
///It handles refreshing as needed to makes sure all data is correct visually
///</summary>
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public Inventory playerInventory;
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    public Transform slotContainer;


    private void Awake()
    {
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }


    //we use this to make sure that any time it is changed
    //in any way, it stays updated
    public void RefreshInventoryUI()
    {
        //print("Refreshing UI");
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerInventory.slots.Count; i++)
        {
            var slotGO = Instantiate(slotPrefab, slotContainer);
            var slotUI = slotGO.GetComponent<InventorySlotUI>();

            slotUI.slotIndex = i;
            slotUI.isHotbarSlot = false;
            slotUI.inventory = playerInventory;

            InventorySlot slotData = playerInventory.slots[i];
            if (slotData.isSelected)
            {
                slotGO.GetComponent<Image>().color = Color.black;
            }
            else
            {
                slotGO.GetComponent<Image>().color = Color.white;
            }
            if (slotData.itemData != null)
            {
                // Only add visual item to slot if there's actual item data
                var itemGO = Instantiate(itemPrefab, slotGO.transform);
                var dragItem = itemGO.GetComponent<DraggableItem>();
                dragItem.source = DraggableItem.Source.MainInventory;
                dragItem.itemData = slotData.itemData;
                dragItem.amount = slotData.amount;
                dragItem.image.sprite = slotData.itemData.icon;
                dragItem.parentAfterDrag = slotGO.transform;
                dragItem.slotIndex = i;// <= new line
                                       // dragItem.sourceInventory = DraggableItem.InventoryType.MainInventory;
                if (dragItem.amountText != null)
                    dragItem.amountText.text = dragItem.amount > 1 ? dragItem.amount.ToString() : "";

            }
            else
            {
                //var image = slotGO.GetComponent<UnityEngine.UI.Image>();
                //if (image != null)
                //{
                //    // Set an "empty slot" sprite, like a faint box or a grid square
                //    image.sprite = null;
                //    image.color = Color.white; // Make sure it's visible
                //}
            }
        }

    }



}

