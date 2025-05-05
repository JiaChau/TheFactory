using UnityEngine.EventSystems;
using UnityEngine;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggable = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (draggable == null) return;

        IDropReceiver receiver = GetComponent<IDropReceiver>();
        if (receiver != null)
        {
            receiver.HandleItemDrop(draggable.itemData, draggable.amount); // Send full stack
            Destroy(draggable.gameObject); // Fully remove the original from inventory UI
        }
        else
        {
            Debug.Log("No valid drop receiver on this object.");
        }
    }
}
