using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Services;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SApp : IHasCustomStyle,IHasContent
{
    [Inject]
    private SecitsApp App { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-app", Class);
    }

    protected void OnClick(MouseEventArgs args)
    {
        App.Click(this, args);
    }
}