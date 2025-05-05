using UnityEngine;
using System.Collections;

public class ResourceNode : MonoBehaviour
{
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

    [Header("Visual Object (Mesh + Collider)")]
    public GameObject visuals; // assign the child GameObject containing the mesh + collider

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
        if (isHarvested || tool.toolType != requiredToolType || tool.toolTier < requiredToolTier)
            return;

        float prevPercent = currentHP / maxHP;
        currentHP -= tool.damage;
        float newPercent = currentHP / maxHP;
        float percentDelta = prevPercent - newPercent;

        DropItems(percentDelta);

        if (currentHP <= 0f)
        {
            StartCoroutine(Respawn());
        }
    }

    void DropItems(float percent)
    {
        for (int i = 0; i < dropItems.Length; i++)
        {
            int totalToDrop = Mathf.CeilToInt(dropAmounts[i] * percent);
            for (int j = 0; j < totalToDrop; j++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-dropRadius, dropRadius),
                    0f,
                    Random.Range(-dropRadius, dropRadius)
                );

                Vector3 dropPos = transform.position + randomOffset;

                GameObject drop = Instantiate(dropPrefab, dropPos, Quaternion.identity);
                PickUpItem pickup = drop.GetComponent<PickUpItem>();
                if (pickup)
                    pickup.itemData = dropItems[i];
            }
        }
    }

    IEnumerator Respawn()
    {
        isHarvested = true;
        if (visuals != null)
            visuals.SetActive(false);

        yield return new WaitForSeconds(respawnTime);

        currentHP = maxHP;
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
