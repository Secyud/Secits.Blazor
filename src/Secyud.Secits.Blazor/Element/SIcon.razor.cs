using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SIcon : IHasCustomCss, ICanClick
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? IconName { get; set; }

    [Parameter]
    public EventCallback Click { get; set; }

    protected void OnClick(MouseEventArgs args)
    {
        if (Click.HasDelegate)
            Click.InvokeAsync(args).ConfigureAwait(false);
    }
}