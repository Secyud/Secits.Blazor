using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SFloater : IHasContent, IHasCustomStyle
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-floater", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }
}