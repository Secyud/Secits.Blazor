using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings.Tabs;

namespace Secyud.Secits.Blazor;

public partial class TabContentView : IHasCustomCss
{
    [CascadingParameter]
    public TabContainer? TabContainer { get; set; }

    [Parameter]
    public bool SaveRenderState { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-tab-contents", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }


    protected RenderFragment RenderTabContents(IReadOnlyList<ITab> tabs) =>
        builder =>
        {
            foreach (var tab in tabs)
            {
                builder.AddContent(tab.Index, tab.RenderTabContent());
            }
        };
}