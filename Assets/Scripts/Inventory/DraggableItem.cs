///<summary>
///This goes on the imageItemsForInventory and aids in them actually BEING clickable & draggable 
///it also manages snapping back to it's old parent if it doesn't land where it was meant to
///</summary>
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//subscriping to the following events helps with the process then calling them
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;

    public Image image;

    // Reference to the inventory data for this item
    public ItemData itemData;
    public int amount;
    public int slotIndex; 
    public TMP_Text amountText;

    //clicking
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        //brought this in so it is drawn above all canvases
        transform.SetParent(CanvasManagers.Instance.dragCanvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    //dragging
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    //stop dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    //this helps with the fuel burning, since the generator can reference this
    public void RemoveAmount(int amt)
    {
        amount -= amt;
        if (amountText != null)
            amountText.text = amount > 1 ? amount.ToString() : "";

        if (amount <= 0)
            Destroy(gameObject);
    }
}
