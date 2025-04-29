////using UnityEngine;

////public class ItemClass : MonoBehaviour
////{
////    public ItemData itemData;

////    private void Awake()
////    {
////        GetComponent<MeshFilter>().mesh = itemData.mesh;
////    }
////}
//using UnityEngine;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
//public class ItemClass : MonoBehaviour
//{
//    public ItemData itemData;

//    private void Start()
//    {
//        ApplyItemVisuals();
//    }

//    public void ApplyItemVisuals()
//    {
//        if (itemData == null) return;

//        var filter = GetComponent<MeshFilter>();
//        var renderer = GetComponent<MeshRenderer>();

//        filter.mesh = itemData.mesh;
//        //renderer.material = itemData.material;
//    }

//    public void SetItem(ItemData newItem)
//    {
//        itemData = newItem;
//        ApplyItemVisuals();
//    }
//}
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

    public void ApplyItemVisuals()
    {
        if (itemData == null) return;

        var filter = GetComponent<MeshFilter>();
        var renderer = GetComponent<MeshRenderer>();

        filter.mesh = itemData.mesh;
        itemData.SetAmount(amount);
        //renderer.material = itemData.material;
    }

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
