//using UnityEngine;
//using UnityEngine.EventSystems;

//public class InventorySlot : MonoBehaviour, IDropHandler
//{
//    public void OnDrop(PointerEventData eventData)
//    {
//        if (transform.childCount == 0)
//        {
//            GameObject dropped = eventData.pointerDrag;
//            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
//            draggableItem.parentAfterDrag = transform;
//        }
//    }
//}

[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int amount;

    public InventorySlot(ItemData data, int amt = 1)
    {
        itemData = data;
        amount = amt;
    }

    public bool CanStackWith(ItemData other)
    {
        return itemData != null && itemData == other;
    }

    public void AddAmount(int amt)
    {
        amount += amt;
    }
}
