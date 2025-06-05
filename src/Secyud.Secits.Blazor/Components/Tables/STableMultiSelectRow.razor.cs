namespace Secyud.Secits.Blazor.Components;

public partial class STableMultiSelectRow<TItem> : ISciRowRenderer<TItem>
{
    protected override void ApplySetting()
    {
        Master?.SetRowRender(this);
    }

    protected override void ForgoSetting()
    {
        Master?.UnsetRowRender(this);
    }
}