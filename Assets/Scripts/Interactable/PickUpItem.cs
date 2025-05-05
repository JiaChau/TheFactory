using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public ItemData itemData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                bool added = InventoryManager.Instance.AddItem(itemData, itemData.amount);
                if (added)
                {
                    // Optional: play VFX/SFX here

                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory full!");
                }
            }
        }
    }
}
