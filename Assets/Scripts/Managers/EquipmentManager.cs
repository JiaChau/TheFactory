using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }
    public Transform playerHand;
    public Inventory inventory;

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
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) && !inventory.GetSelectedItem().IsEmpty)
                {
                    EquipmentManager.Instance.Equip(inventory.GetSelectedItem().itemData);
                }
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
        //THis checks if it is using the child script of craftable scriptable object
        if(item is CraftableData craftable)
        {
            UnEquip();
            GameObject GO = craftable.prefabCreatedInWorld;
            if(GO.GetComponent<PickUpItem>() != null)
            {
                GO.GetComponent<PickUpItem>().enabled = false;
                if(GO.GetComponent<SphereCollider>().isTrigger)
                    GO.GetComponent<SphereCollider>().enabled = false;
            }
            Instantiate(GO, playerHand);
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
