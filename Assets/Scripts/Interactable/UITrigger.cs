using UnityEngine;

public class UITrigger : MonoBehaviour
{
    private bool inRange = false;

    private void Update()
    {
        if (!inRange)
        {
            return;
        }

        // Show interaction prompt only if canvas is not already open
        if (!CanvasManagers.Instance.isSecondaryCanvasOpen)
        {
            TutorialManager.Instance.SetTutorialCanvas("E to Interact");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            print("E Pressed");
            TutorialManager.Instance.TurnOffTutorialCanvas();

            if (!CanvasManagers.Instance.isSecondaryCanvasOpen)
            {
                print("canvas opened");
                WhichCanvasIsIt();
                CanvasManagers.Instance.OpenSecondaryCanvas(CanvasManagers.Instance.GetSecondaryCanvas(), true);
                Interact();
            }
            else
            {
                print("canvas closed");
                WhichCanvasIsIt();
                CanvasManagers.Instance.CloseSecondaryCanvas(CanvasManagers.Instance.GetSecondaryCanvas(), false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            WhichCanvasIsIt();
            CanvasManagers.Instance.CloseMainInventory();
            TutorialManager.Instance.TurnOffTutorialCanvas();
        }
    }

    // Override this to define what canvas this trigger opens
    public virtual void WhichCanvasIsIt() { }

    // Override to define custom interaction logic
    public virtual void Interact() { }
}
