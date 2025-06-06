namespace Secyud.Secits.Blazor.Components;

public partial class STableMultiSelectRow<TItem> : ISciRowRenderer<TItem>
{
    protected override void ApplySetting()
    {
        Master.RowRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.RowRenderer.Forgo(this);
    }
}