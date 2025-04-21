using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory inventory;
    private void Awake()
    {
        inventory = new Inventory();
    }
}
