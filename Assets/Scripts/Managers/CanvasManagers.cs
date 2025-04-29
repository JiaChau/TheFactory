using Unity.VisualScripting.Antlr3.Runtime;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMainInventory()
    {
        inventoryCanvas.SetActive(false);
        isInventoryOpen = false;
        if (isSecondaryCanvasOpen)
        {
            CloseSecondaryCanvas(GetSecondaryCanvas(), false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void OpenSecondaryCanvas(GameObject canvas, bool currentBool)
    {
        if (!isInventoryOpen)
        {
           OpenMainInventory();
        }
        canvas.SetActive(currentBool);
        isSecondaryCanvasOpen = currentBool;

    }

    public void CloseSecondaryCanvas(GameObject canvas, bool currentBool)
    {
        if (isInventoryOpen)
        {
            CloseMainInventory();
        }
        canvas.SetActive(currentBool);
        isSecondaryCanvasOpen = currentBool;
    }


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

    public void SetSecondaryCanvas(SecondaryCanvas canvas)
    {
        canvasType = canvas;
    }

    public void SecondaryButtonClose()
    {
        isSecondaryCanvasOpen = false;
    }

}


    

