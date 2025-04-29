using UnityEngine;

public class GeneratorUITrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!CanvasManagers.Instance.isSecondaryCanvasOpen)
                TutorialManager.Instance.SetTutorialCanvas("E to Interact");
            if (Input.GetKeyDown(KeyCode.E))
            {
                TutorialManager.Instance.TurnOffTutorialCanvas();
                if (!CanvasManagers.Instance.isSecondaryCanvasOpen)
                {
                    CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.Generator);
                    CanvasManagers.Instance.OpenSecondaryCanvas(CanvasManagers.Instance.GetSecondaryCanvas(),
                        true);
                    
                }
                else
                {
                    CanvasManagers.Instance.CloseSecondaryCanvas(CanvasManagers.Instance.GetSecondaryCanvas(),false);
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


    

}
