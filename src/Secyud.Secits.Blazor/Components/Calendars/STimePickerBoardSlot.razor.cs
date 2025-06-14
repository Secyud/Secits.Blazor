namespace Secyud.Secits.Blazor;

public partial class STimePickerBoardSlot
{
    protected override void ApplySetting()
    {
        Master.ValueContainer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ValueContainer.Forgo(this);
    }
}