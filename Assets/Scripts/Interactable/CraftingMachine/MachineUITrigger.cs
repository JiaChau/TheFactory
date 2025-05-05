public class MachineUITrigger : UITrigger
{
    public CraftingMenu craftingMenu;
    bool listCreated = false;
    public override void WhichCanvasIsIt()
    {
        CanvasManagers.Instance.SetSecondaryCanvas(CanvasManagers.SecondaryCanvas.CraftingMachine);
    }

    public override void Interact()
    {
        if (!listCreated)
            MachineUI.Instance.RefreshMachineUI();
            craftingMenu.GenerateCraftingList();
    }
}
