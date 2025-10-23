using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class AddTemplate : SLayoutPluginBase<SActivableBase>, IHasContent, IContentRenderer
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RendererPosition Position { get; set; } = RendererPosition.Body;

    public override RendererPosition GetLayoutPosition()
    {
        return Position;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, ChildContent);
    }
}