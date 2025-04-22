///<summary>
///This is put on the generator object itself
///to handle WHEN the player can open and close the generators canvas 
///based on the sphere collision
///Seems messy, but on optimization, it could become a parent class
///</summary>
using UnityEngine;

public class GeneratorUITrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //manages whether we see the "interact" prompt
            //(We don't want to see it if the panels are open)
            if(!CanvasManagers.Instance.isSecondaryCanvasOpen)
                TutorialManager.Instance.SetTutorialCanvas("E to Interact");

            if (Input.GetKeyDown(KeyCode.E))
            {
                TutorialManager.Instance.TurnOffTutorialCanvas();
                
                if (!CanvasManagers.Instance.isSecondaryCanvasOpen)
                {
                    //setting our enum of which second canvs is open
                    CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.Generator);
                    //then getting which secondary canvas it is to pass it into this function
                    CanvasManagers.Instance.OpenSecondaryCanvas(
                        CanvasManagers.Instance.GetSecondaryCanvas(),true
                        );
                    
                }
                else
                {
                    //same logic, but to close it
                    CanvasManagers.Instance.CloseSecondaryCanvas(
                        CanvasManagers.Instance.GetSecondaryCanvas(),false
                        );
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if we walk away we want to close everything
        if (other.CompareTag("Player"))
        {
            CanvasManagers.Instance.CloseMainInventory();
            TutorialManager.Instance.TurnOffTutorialCanvas();

        }
    }
}
