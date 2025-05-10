using UnityEngine;

public class PlayerGathering : MonoBehaviour
{
    public Transform cam; // Assign the player's camera
    public GameObject equippedTool; // Assign currently equipped tool prefab
    public float hitCooldown = 0.5f; // Cooldown time between hits in seconds
    private float lastHitTime = 0f;
    bool canSwing = true;

   // bool canHarvest = true;
    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canSwing)
        {
            if (equippedTool == null) return; // Skip if no tool equipped
            
            if (Time.time >= lastHitTime + hitCooldown)
            {
                SwingTool();
                // Exclude "Pickup" layer from raycast
                int layerMask = ~(1 << LayerMask.NameToLayer("Pickup"));

                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 3f, layerMask))
                {
                    ResourceNode node = hit.collider.GetComponentInParent<ResourceNode>();

                    Tool tool = equippedTool.GetComponent<Tool>();

                    if (node && tool)
                    {
                        node.Hit(tool);
                        node.ShowHitEffect();
                        lastHitTime = Time.time;
                        PlaySoundByToolType(tool);
                    }
                }
            }

        }
    }

    //These functions are to allow for swinging animations
    public void SwingTool()
    {
        playerAnimator.SetTrigger("Harvest");
        canSwing = false;
    }

    //this is called in animation at the end as AnimationEvent
    public void ResetSwing()
    {
        canSwing = true;
    }

    void PlaySoundByToolType(Tool currentTool)
    {
        switch (currentTool.ToolType)
        {
            case ToolType.Axe:
                SoundManager.Instance.PlayWoodChopSound();
                break;
            case ToolType.Pickaxe:
                SoundManager.Instance.PlayPickAxeSound();
                break;
        }

    }


}
