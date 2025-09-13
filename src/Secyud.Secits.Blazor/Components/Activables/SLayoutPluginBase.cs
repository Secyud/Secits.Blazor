using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class SLayoutPluginBase<TComponent> : SPluginBase<TComponent>,
    IContentRenderer
    where TComponent : SPluggableBase, IHasLayoutTemplateSlot
{
    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Class { get; set; }

    public abstract RendererPosition GetLayoutPosition();

    public abstract RenderFragment RenderTemplate();

    protected virtual string? GetStyle()
    {
        return Style;
    }

    protected virtual string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("anim", Class);
    }

    protected override void ApplySetting()
    {
        Master.SlotRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.SlotRenderer.Forgo(this);
    }
}