using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class SLayoutSettingBase<TComponent> : SSettingBase<TComponent>,
    ILayoutTemplateRenderer
    where TComponent : SComponentBase,IHasLayoutTemplateSlot
{
    [Parameter]
    public string? Col { get; set; }

    [Parameter]
    public string? Row { get; set; }

    public abstract RenderFragment RenderTemplate();

    protected string GetStyle()
    {
        return $"grid-column: {Col}; grid-row: {Row};";
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