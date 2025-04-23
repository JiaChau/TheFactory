///<summary>
///This is a singleton that handles all the canvases. 
///It will manage whether you can look around ***See Mouse Movement
///There are only Two Bools, but there might be more canvases in the future
///(maybe even an array built or a list, we'll see), for now it is to be managed with an enum
///</summary>
using UnityEngine;

public class CanvasManagers : MonoBehaviour
{
    
    public bool isInventoryOpen;
    public bool isSecondaryCanvasOpen;
    public GameObject dragCanvas;
    [SerializeField]
    GameObject generatorCanvas;
    [SerializeField] 
    GameObject inventoryCanvas;
    public static CanvasManagers Instance;

    //enum to be expanded for secondary canvases
    public enum SecondaryCanvas
    {
        Generator
    }
    public SecondaryCanvas canvasType;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {

        inventoryCanvas.SetActive(false);
        generatorCanvas.SetActive(false);
        isInventoryOpen = false;
        isSecondaryCanvasOpen = false;
    }

    //checks if I is pressed at any point to open or closes
    //Main Inventory
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInventoryOpen)
            {
                OpenMainInventory();
            }
            else
            {
                CloseMainInventory();
            }

        }
    }

    
    public void OpenMainInventory()
    {
        inventoryCanvas.SetActive(true);
        isInventoryOpen = true;
        //this gives us control of the mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMainInventory()
    {
        //if any of the potential secondary canvases are open
        //close them
        if (isSecondaryCanvasOpen)
        {
            CloseSecondaryCanvas(GetSecondaryCanvas(), false);
        }
        inventoryCanvas.SetActive(false);
        isInventoryOpen = false;
        //this removes control of the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    //To open the secondary, we need to know which one
    //this will be useful if/when we expand
    public void OpenSecondaryCanvas(GameObject canvas, bool currentBool)
    {
        //to make sure both open at once
        if (!isInventoryOpen)
        {
           OpenMainInventory();
        }
        canvas.SetActive(currentBool);
        isSecondaryCanvasOpen = currentBool;

    }

    /** 
     * Was a infinite recursive call...
     * Same concept as above but backwards
    public void CloseSecondaryCanvas(GameObject canvas, bool currentBool)
    {
        //to make sure both close at once (might change)
        if (isInventoryOpen)
        {
            CloseMainInventory();
        }
        canvas.SetActive(currentBool);
        isSecondaryCanvasOpen = currentBool;
    }
    **/
    public void CloseSecondaryCanvas(GameObject canvas, bool currentBool)
    {
        // 1) actually close the secondary canvas
        canvas.SetActive(currentBool);
        isSecondaryCanvasOpen = currentBool;    // now false

        // 2) if the inventory is still open, close it
        if (isInventoryOpen)
            CloseMainInventory();
    }


    //this is a switch that checks the canvas type
    public GameObject GetSecondaryCanvas()
    {
        switch (canvasType)
        {
            case SecondaryCanvas.Generator:
                return generatorCanvas;
            default:
                break;
        }
        return null;
    }

    //sets the canvasType
    public void SetSecondaryCanvas(SecondaryCanvas canvas)
    {
        canvasType = canvas;
    }

    //this is here because I added close buttons to the menus
    //and I needed to also flip the boolean to false
    public void SecondaryButtonClose()
    {
        isSecondaryCanvasOpen = false;
    }

}


    

