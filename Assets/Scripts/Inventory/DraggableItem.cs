///<summary>
///This goes on the imageItemsForInventory and aids in them actually BEING clickable & draggable 
///it also manages snapping back to it's old parent if it doesn't land where it was meant to
///</summary>
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//subscriping to the following events helps with the process then calling them
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [HideInInspector] public Transform parentAfterDrag;

    public Image image;
    //private CanvasGroup canvasGroup;
    // Reference to the inventory data for this item
    public ItemData itemData;
    public int amount;
    public int slotIndex;
    public TMP_Text amountText;
    private CanvasGroup canvasGroup;

    public enum Source
    {
        MainInventory,
        CraftingMachine
    }
    public Source source;

    private void Awake()
    {
        //amountText.text = "";
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Important!
    }

    //clicking
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponentInParent<InventorySlotUI>() != null)
        {
            if (GetComponentInParent<InventorySlotUI>().isHotbarSlot)
                return;
        }


        parentAfterDrag = transform.parent;
        //brought this in so it is drawn above all canvases
        transform.SetParent(CanvasManagers.Instance.dragCanvas.transform);
        transform.SetAsLastSibling();
        slotIndex = InventoryManager.Instance.GetSlotByIndex(slotIndex);
        //image.raycastTarget = false;
        canvasGroup.blocksRaycasts = false;


    }
    //dragging
    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponentInParent<InventorySlotUI>() != null)
        {
            if (GetComponentInParent<InventorySlotUI>().isHotbarSlot)
                return;
        }
        //if (!GetComponentInParent<InventorySlotUI>().isHotbarSlot)
        transform.position = Input.mousePosition;
        // print("Raycast blocking: " + canvasGroup.blocksRaycasts + "transform parent: " + transform.parent.gameObject.name);
    }

    //stop dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GetComponentInParent<InventorySlotUI>() != null)
        {
            if (GetComponentInParent<InventorySlotUI>().isHotbarSlot)
                return;
        }
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true; // <<< Turn raycasts back on

        //if (canvasGroup != null)
        //    canvasGroup.blocksRaycasts = true;
        //image.raycastTarget = true;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && GetComponentInParent<InventorySlotUI>().isHotbarSlot)
        {
            print("Right Button clicked");
            InventoryManager.Instance.inventory.RemoveItemFromHotBar(slotIndex);

        }
        else if (eventData.button == PointerEventData.InputButton.Left && !GetComponentInParent<InventorySlotUI>().isHotbarSlot)
        {
            print("Left Button clicked");
            InventoryManager.Instance.inventory.SelectSlot(slotIndex);
            InventoryUI.Instance.RefreshInventoryUI();
        }

    }
}
