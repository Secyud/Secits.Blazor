using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class AddLabel : SLayoutPluginBase<SActivableBase>
{
    [Parameter]
    public RendererPosition RendererPosition { get; set; } = RendererPosition.Body;

    [Parameter]
    public string? For { get; set; }

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string? Text { get; set; }

    public override RendererPosition GetLayoutPosition()
    {
        return RendererPosition;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "label");
        builder.AddAttributeIfNotEmpty(1, "for", For);
        builder.AddAttributeIfNotEmpty(2, "name", Name);
        builder.AddContent(5, Text);
        builder.CloseElement();
    }
}