using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }
    public Transform playerHand;
    public Inventory inventory;
    public PlayerGathering playerGathering; // I added -Jia

    private void Awake()
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
    void Update()
    {
      
        if (!CanvasManagers.Instance.isInventoryOpen)
        {
            for (int i = 0; i < inventory.hotBarSlots.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) && !inventory.slots[i].IsEmpty) 
                {
                    if(!inventory.GetSelectedItem().IsEmpty)
                    {
                        Equip(inventory.GetSelectedItem().itemData);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UnEquip();
            }
        }
        else
        {
            for (int i = 0; i < inventory.hotBarSlots.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) && !inventory.GetSelectedItem().IsEmpty)
                {
                    inventory.AddItemToHotBar(inventory.GetSelectedItem(), i);
                }
            }
            
        }
        

    }
    public void Equip(ItemData item)
    {
        // Check if the item is a craftable tool with a prefab
        if (item is CraftableData craftable)
        {
            UnEquip(); // Remove previous tool

            GameObject GO = craftable.prefabCreatedInWorld;

            // Disable pickup logic if present
            if (GO.GetComponent<PickUpItem>() != null)
            {
                GO.GetComponent<PickUpItem>().enabled = false;

                if (GO.GetComponent<SphereCollider>()?.isTrigger == true)
                    GO.GetComponent<SphereCollider>().enabled = false;
            }

            // Instantiate the tool at the player's hand
            GameObject spawnedTool = Instantiate(GO, playerHand);
            spawnedTool.transform.localPosition = Vector3.zero;
            spawnedTool.transform.localRotation = Quaternion.identity;

            // Set damage from CraftableData
            Tool tool = spawnedTool.GetComponent<Tool>();
            if (tool != null)
            {
                tool.Initialize(craftable); // Fully configures tool with ScriptableObject data
            }


            // Update PlayerGathering reference to enable gathering logic
            if (playerGathering != null)
            {
                playerGathering.equippedTool = spawnedTool;
            }

        }
    }


    public void UnEquip()
    {
        foreach (Transform child in playerHand.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

