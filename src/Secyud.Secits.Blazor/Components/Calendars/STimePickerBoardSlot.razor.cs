namespace Secyud.Secits.Blazor.Components;

public partial class STimePickerBoardSlot
{
    protected override void ApplySetting()
    {
        Master.InputSlotRenderers.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.InputSlotRenderers.Forgo(this);
    }
}