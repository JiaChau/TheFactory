using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct OutputMapping
{
    /// <summary>
    /// Fuel type to watch for.
    /// </summary>
    public ItemData fuelInput;

    /// <summary>
    /// How many fuel units must be consumed before emitting output.
    /// </summary>
    public int unitsPerOutput;

    /// <summary>
    /// How many distinct product GameObjects to spawn each time.
    /// </summary>
    public int spawnCount;

    /// <summary>
    /// How many “items” each spawned GameObject carries.
    /// </summary>
    public int amountPerProduct;

    /// <summary>
    /// Prefab to instantiate.
    /// </summary>
    public GameObject productPrefab;

    /// <summary>
    /// Reference to the item data for dynamic adjustments.
    /// </summary>
    public ItemData itemData; // New reference for dynamic item data
}

public class GeneratorOutput : MonoBehaviour
{
    [Header("Spawn Locations")]
    public List<Transform> waypoints;

    [Header("Fuel to Output Rules")]
    public List<OutputMapping> mappings;

    private GeneratorFuelBurner burner;
    private int nextWaypoint = 0;
    private Dictionary<string, int> burnAccumulators = new Dictionary<string, int>();

    void Awake()
    {
        burner = GetComponent<GeneratorFuelBurner>();
        if (burner == null)
            Debug.LogError("Missing GeneratorFuelBurner");
        burner.OnFuelUnitBurned += HandleFuelBurned;
    }

    void OnDestroy()
    {
        if (burner != null)
            burner.OnFuelUnitBurned -= HandleFuelBurned;
    }

    private void HandleFuelBurned(ItemData fuel)
    {
        var map = mappings.FirstOrDefault(m => m.fuelInput == fuel);
        if (map.productPrefab == null)
        {
            Debug.LogWarning("No mapping for fuel " + fuel.itemType);
            return;
        }

        string key = fuel.itemType.ToString();
        if (!burnAccumulators.ContainsKey(key))
            burnAccumulators[key] = 0;
        burnAccumulators[key]++;

        if (burnAccumulators[key] < map.unitsPerOutput)
            return;

        burnAccumulators[key] -= map.unitsPerOutput;
        SpawnProducts(map);
    }

    private void SpawnProducts(OutputMapping map)
    {
        for (int i = 0; i < map.spawnCount; i++)
        {
            if (waypoints.Count == 0) break;
            var wp = waypoints[nextWaypoint];
            GameObject go = Instantiate(map.productPrefab, wp.position, wp.rotation);

            var ic = go.GetComponent<ItemClass>();
            if (ic != null)
            {
                ic.amount = map.amountPerProduct;
                ic.itemData = map.itemData;
                ic.ApplyItemVisuals();
            }

            nextWaypoint = (nextWaypoint + 1) % waypoints.Count;
        }
    }
}
