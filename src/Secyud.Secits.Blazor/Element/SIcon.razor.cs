using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Element;

public partial class SIcon : IHasCustomCss
{
    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? IconName { get; set; }
    
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }
}