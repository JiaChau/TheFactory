/// <summary>
/// Manages the burning of fuel in a generator. Handles finding fuel, 
/// starting the burn process, consuming fuel, and stopping when fuel is depleted.
/// </summary>
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneratorFuelBurner : MonoBehaviour
{
    [Header("UI Fuel Slots")]
    [Tooltip("List of fuel slots where fuel items can be placed.")]
    public List<Transform> fuelSlots; // Drop the slots in the inspector

    /// <summary>
    /// Fires whenever we finish burning one unit of fuel.
    /// Listeners can spawn products, play sounds, etc.
    /// </summary>
    public event Action<ItemData> OnFuelUnitBurned;

    private DraggableItem currentFuel;
    private float burnTimer = 0f;
    private bool isBurning = false;
    private bool isOn = false;

    private void Update()
    {
        if (isBurning)
        {
            burnTimer -= Time.deltaTime;
            if (burnTimer <= 0f)
            {
                TryConsumeFuel();
            }
        }
    }

    // Starts burning the given fuel item based on the specific item's time
    private void StartBurn(DraggableItem fuelItem)
    {
        float duration = GetBurnDuration(fuelItem.itemData);
        burnTimer = duration;
        isBurning = true;
        Debug.Log($"🔥 Started burning {fuelItem.itemData.itemType} for {duration} seconds");
    }

    //consumes the fuel, if possible, if not stops burning
    private void TryConsumeFuel()
    {
        if (currentFuel != null)
        {
            currentFuel.RemoveAmount(1);
            OnFuelUnitBurned?.Invoke(currentFuel.itemData);

            if (currentFuel.amount > 0)
            {
                burnTimer = GetBurnDuration(currentFuel.itemData);
                Debug.Log($"🔥 Continuing to burn {currentFuel.itemData.itemType}");
            }
            else
            {
                currentFuel = FindValidFuel();
                if (currentFuel != null)
                {
                    StartBurn(currentFuel);
                }
                else
                {
                    TurnOffGenerator();
                    Debug.Log("🔥 Generator stopped — out of fuel.");
                }
            }
        }
    }


    //this searches all the slots for any valid fuel item to burn
    private DraggableItem FindValidFuel()
    {
        foreach (Transform slot in fuelSlots)
        {
            DraggableItem item = slot.GetComponentInChildren<DraggableItem>();
            if (item != null && item.itemData.fuelProperties.isFuel && item.amount > 0)
            {
                return item;
            }
        }
        return null;
    }

    //simply retuns how much longer an item can burn
    private float GetBurnDuration(ItemData data)
    {
        return data.fuelProperties != null && data.fuelProperties.isFuel
            ? data.fuelProperties.burnDuration
            : 0f;
    }


    public void GeneratorButton()
    {
        if (!isOn)
        {
            currentFuel = FindValidFuel();
            if (currentFuel != null)
            {
                SoundManager.Instance.PlayGeneratorSound();
                isOn = true;
                StartBurn(currentFuel);

            }
            else
            {
                Debug.Log("❌ No fuel to start the generator.");
            }
        }
        else
        {
            TurnOffGenerator();
        }
    }


    public void TurnOffGenerator()
    {
        isBurning = false;
        isOn = false;
        SoundManager.Instance.StopCraftingSound();
    }

    public bool GetStateOfGenerator()
    {
        return isOn;
    }
}