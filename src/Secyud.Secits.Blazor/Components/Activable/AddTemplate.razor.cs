using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class AddTemplate : IHasContent,ILayoutTemplateRenderer
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void ApplySetting()
    {
        Master.SlotRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.SlotRenderer.Forgo(this);
    }
}