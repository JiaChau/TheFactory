using UnityEngine;
using System.Collections;

public class ResourceNode : MonoBehaviour
{
    public enum ResourceType { Tree, Rock, Crop }

    [Header("Resource Type")]
    public ResourceType resourceType = ResourceType.Tree;

    [Header("Tool Requirements")]
    public ToolType requiredToolType;
    public ToolTier requiredToolTier = ToolTier.Basic;

    [Header("Optional Hit Visual")]
    public GameObject hitEffectPrefab;
    public Transform hitEffectSpawnPoint;

    [Header("Health & Respawn")]
    public float maxHP = 100f;
    public float respawnTime = 60f;
    private float currentHP;
    private bool isHarvested = false;

    [Header("Drop Settings")]
    public ItemData[] dropItems;
    public int[] dropAmounts;
    public GameObject dropPrefab;
    public float dropRadius = 1.5f;
    private float lastDropPercent = 1f;
    private const float dropThreshold = 0.15f;

    [Header("Visual Object (Mesh + Collider)")]
    public GameObject visuals;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        currentHP = maxHP;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Hit(Tool tool)
    {
        if (isHarvested)
            return;

        if (tool.ToolType != requiredToolType)
        {
            Debug.Log("Invalid tool type");
            return;
        }

        if (tool.ToolTier < requiredToolTier)
            return;

        if (resourceType == ResourceType.Crop)
        {
            int fortuneMultiplier = Mathf.Clamp((int)tool.ToolTier + 1, 1, 5);
            DropItems(fortuneMultiplier); // Drop all at once, multiplied
            StartCoroutine(Respawn());
        }
        else
        {
            float oldPercent = currentHP / maxHP;
            currentHP -= tool.Damage;
            currentHP = Mathf.Max(0, currentHP);
            float newPercent = currentHP / maxHP;

            while (lastDropPercent - dropThreshold >= newPercent)
            {
                lastDropPercent -= dropThreshold;
                DropItems(0.15f);
            }

            if (currentHP <= 0f)
            {
                StartCoroutine(Respawn());
            }
        }
    }


    // Overload for HP % based drop
    void DropItems(float percent)
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            int totalToDrop = Mathf.CeilToInt(dropAmounts[i] * percent);
            SpawnDrops(dropItems[i], totalToDrop);
        }
    }

    // Overload for fortune multiplier
    void DropItems(int multiplier)
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            int totalToDrop = dropAmounts[i] * multiplier;
            SpawnDrops(dropItems[i], totalToDrop);
        }
    }

    void SpawnDrops(ItemData item, int count)
    {
        for (int j = 0; j < count; j++)
        {
            Vector3 offset;
            do
            {
                offset = new Vector3(
                    Random.Range(-dropRadius, dropRadius),
                    0f,
                    Random.Range(-dropRadius, dropRadius)
                );
            } while (offset.magnitude < dropRadius * 0.5f);

            Vector3 dropPos = transform.position + offset;
            dropPos.y = dropPrefab.transform.position.y;

            GameObject drop = Instantiate(dropPrefab, dropPos, Quaternion.identity);
            PickUpItem pickup = drop.GetComponent<PickUpItem>();
            if (pickup)
                pickup.itemData = item;
        }
    }

    IEnumerator Respawn()
    {
        isHarvested = true;
        if (visuals != null)
            visuals.SetActive(false);

        yield return new WaitForSeconds(respawnTime);

        currentHP = maxHP;
        lastDropPercent = 1f;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        if (visuals != null)
            visuals.SetActive(true);

        isHarvested = false;
    }

    public void ShowHitEffect()
    {
        if (hitEffectPrefab)
        {
            Vector3 spawnPos = hitEffectSpawnPoint ? hitEffectSpawnPoint.position : transform.position;
            Instantiate(hitEffectPrefab, spawnPos, Quaternion.identity);
        }
    }
}
