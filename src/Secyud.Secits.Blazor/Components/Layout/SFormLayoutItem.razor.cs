using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class SFormLayoutItem : SLayoutPluginBase<SFormLayout>, IHasContent
{
    [Parameter]
    public int ColSpan { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Title { get; set; }
}