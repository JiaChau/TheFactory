///<summary>
///So, this goes on the ItemForInventoryInWorld and that this does is:
///uses the ItemDataBase Scriptable Object AS WELL AS the 
///ItemData Scriptable Object to create a single prefab that can change on the fly
///to whatever item we need with the desired data we want to give it
///</summary>
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ItemClass : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public ItemData.ItemType selectedType;
    public int amount;

    [HideInInspector] public ItemData itemData;

    private void OnValidate()
    {
        if (itemDatabase != null)
        {
            itemData = itemDatabase.GetItemByType(selectedType);

#if UNITY_EDITOR
            // Delay mesh assignment to avoid "SendMessage" error
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (this != null) ApplyItemVisuals();
            };
#else
        ApplyItemVisuals();
#endif
        }
    }

    //desplays the mesh and calls the set amount
    public void ApplyItemVisuals()
    {
        if (itemData == null) return;

        var filter = GetComponent<MeshFilter>();
        var renderer = GetComponent<MeshRenderer>();

        filter.mesh = itemData.mesh;
        itemData.SetAmount(amount);
        //renderer.material = itemData.material;
    }

    //handles the pick-up on collision (no button for now)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                bool added = InventoryManager.Instance.AddItem(itemData, amount);
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
