using UnityEngine;

public class GeneratorUITrigger : UITrigger 
{

    public override void WhichCanvasIsIt()
    {
        CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.Generator);
    }

}
