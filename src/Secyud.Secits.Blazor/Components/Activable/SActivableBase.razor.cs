using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SActivableBase
{
    #region Parameters

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    #endregion

    #region Settings

    public SSettings<ILayoutTemplateRenderer> SlotRenderer { get; } = new();

    #endregion
}