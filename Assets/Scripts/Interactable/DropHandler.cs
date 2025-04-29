//using UnityEngine;
//using UnityEngine.EventSystems;

//public class DropHandler : MonoBehaviour, IDropHandler
//{
//    public void OnDrop(PointerEventData eventData)
//    {
//        DraggableItem draggable = eventData.pointerDrag?.GetComponent<DraggableItem>();
//        if (draggable == null) return;

//        IDropReceiver receiver = GetComponent<IDropReceiver>();
//        if (receiver != null)
//        {
//            receiver.HandleItemDrop(draggable.itemData, draggable.amount); // or draggable.amount if you're dragging whole stack
//            draggable.RemoveAmount(1);
//            // Optional: destroy or reset UI
//            //Destroy(draggable.gameObject);
//            if (draggable.amountText != null)
//            {
//                draggable.amount -= 1; // or whatever amount you dropped
//                draggable.amountText.text = draggable.amount > 1 ? draggable.amount.ToString() : "";
//            }

//            // Optionally remove it if empty
//            if (draggable.amount <= 0)
//            {
//                Destroy(draggable.gameObject);
//            }
//        }
//        else
//        {
//            Debug.Log("No valid drop receiver on this object.");
//        }
//    }
//}
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
