using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SActivableBase : IHasLayoutTemplateSlot,ICanActive
{
    #region Parameters

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? ColTemplate { get; set; }

    [Parameter]
    public string? RowTemplate { get; set; }

    #endregion

    #region Settings

    public SSettings<ILayoutTemplateRenderer> SlotRenderer { get; } = new();

    #endregion

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendStyle("grid-template-columns", ColTemplate);
        context.AppendStyle("grid-template-rows", RowTemplate);
    }
}