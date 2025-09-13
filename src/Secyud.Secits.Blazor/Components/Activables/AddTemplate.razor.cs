using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class AddTemplate : IHasContent,IContentRenderer
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RendererPosition Position { get; set; } = RendererPosition.Body;

    protected override void ApplySetting()
    {
        Master.SlotRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.SlotRenderer.Forgo(this);
    }

    public RendererPosition GetLayoutPosition()
    {
        return Position;
    }
}