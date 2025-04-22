///<summary>
///A simple class to actually handle items being dropped into the separate UI Canvas
///It uses the interaface IDropReciever to manage the drop. Later we can bring in splitting stacks
///for now it just dumps the whole stack (this goes on whereever you are dropping the items (every slot))
///</summary>
using UnityEngine.EventSystems;
using UnityEngine;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggable = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (draggable == null) 
        { 
            return; 
        }

        IDropReceiver receiver = GetComponent<IDropReceiver>();
        if (receiver != null)
        {
            receiver.HandleItemDrop(draggable.itemData, draggable.amount); // Sends the full stack
            Destroy(draggable.gameObject); // Fully remove the original from inventory UI
        }
        else
        {
            Debug.Log("No valid drop receiver on this object.");
        }
    }
}
