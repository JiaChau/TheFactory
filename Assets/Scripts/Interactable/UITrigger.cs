using UnityEngine;

public class UITrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!CanvasManagers.Instance.isSecondaryCanvasOpen)
                TutorialManager.Instance.SetTutorialCanvas("E to Interact");
            if (Input.GetKeyDown(KeyCode.E))
            {
                TutorialManager.Instance.TurnOffTutorialCanvas();
                if (!CanvasManagers.Instance.isSecondaryCanvasOpen)
                {
                    WhichCanvasIsIt();
                    CanvasManagers.Instance.OpenSecondaryCanvas(CanvasManagers.Instance.GetSecondaryCanvas(),
                        true);
                    Interact();
                }
                else
                {
                    WhichCanvasIsIt();
                    CanvasManagers.Instance.CloseSecondaryCanvas(CanvasManagers.Instance.GetSecondaryCanvas(), false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanvasManagers.Instance.CloseMainInventory();
            TutorialManager.Instance.TurnOffTutorialCanvas();

        }
    }

    public virtual void WhichCanvasIsIt()
    {
        CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.CraftingMachine);
    }

    public virtual void Interact()
    {
        //THis will be changedd as needed!
    }
}
