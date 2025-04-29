//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
//{
//    [HideInInspector]
//    public Transform parentAfterDrag;

//    public Image image;
//    public void OnBeginDrag(PointerEventData eventData)
//    {
//        parentAfterDrag = transform.parent;
//        transform.SetParent(transform.root);
//        transform.SetAsLastSibling();
//        image.raycastTarget = false;
//    }

//    public void OnDrag(PointerEventData eventData)
//    {
//        transform.position = Input.mousePosition;
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        transform.SetParent(parentAfterDrag);
//        image.raycastTarget = true;
//    }
//}
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;

    public Image image;

    // 📦 Reference to the inventory data for this item
    public ItemData itemData;
    public int amount;
    public int slotIndex; 
    public TMP_Text amountText;


    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(CanvasManagers.Instance.dragCanvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void RemoveAmount(int amt)
    {
        amount -= amt;
        if (amountText != null)
            amountText.text = amount > 1 ? amount.ToString() : "";

        if (amount <= 0)
            Destroy(gameObject);
    }
}
