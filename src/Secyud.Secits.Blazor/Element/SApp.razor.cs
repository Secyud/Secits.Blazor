using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Services;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SApp : IHasCustomCss,IHasContent
{
    [Inject]
    private SecitsApp App { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-app", Class);
    }

    protected string? GetStyle()
    {
        return Style;
    }

    protected void OnClick(MouseEventArgs args)
    {
        App.Click(this, args);
    }
}