using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableDropDown : ILayoutTemplateRenderer
{
    protected override void ApplySetting()
    {
        Master.SlotRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.SlotRenderer.Forgo(this);
    }
}