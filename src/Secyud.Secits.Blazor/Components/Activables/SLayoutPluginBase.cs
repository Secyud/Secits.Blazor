using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class SLayoutPluginBase<TComponent> : SPluginBase<TComponent>, IContentRenderer
    where TComponent : SPluggableBase, IHasContentRender
{
    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    public abstract RendererPosition GetLayoutPosition();

    public RenderFragment RenderTemplate() => BuildRenderTree;

    protected virtual string? GetStyle()
    {
        return Style;
    }

    protected virtual string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("anim",
            Readonly ? "readonly" : null,
            Disabled ? "disabled" : null,
            Class);
    }

    protected override void ApplySetting()
    {
        Master.Content.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Content.Forgo(this);
    }
}