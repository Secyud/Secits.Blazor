using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class TabTagView : IHasCustomCss
{
    [CascadingParameter]
    public TabContainer? TabContainer { get; set; }

    [Parameter]
    public string? Class { get; set; }
    [Parameter]
    public string? Style { get; set; }
    
    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-tags", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }
}