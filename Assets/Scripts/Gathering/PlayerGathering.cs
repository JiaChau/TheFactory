using UnityEngine;

public class PlayerGathering : MonoBehaviour
{
    public Transform cam; // Assign the player's camera
    public GameObject equippedTool; // Assign currently equipped tool prefab
    public float hitCooldown = 0.5f; // Cooldown time between hits in seconds
    private float lastHitTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (equippedTool == null) return; // Skip if no tool equipped

            if (Time.time >= lastHitTime + hitCooldown)
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 3f))
                {
                    ResourceNode node = hit.collider.GetComponent<ResourceNode>();
                    Tool tool = equippedTool.GetComponent<Tool>();

                    if (node && tool)
                    {
                        node.Hit(tool);
                        node.ShowHitEffect();
                        lastHitTime = Time.time;
                    }
                }
            }
        }
    }
}

