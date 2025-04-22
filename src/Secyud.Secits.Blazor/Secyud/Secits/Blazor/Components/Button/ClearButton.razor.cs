using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Components;

public partial class ClearButton : IClickComponent
{
    protected override string ComponentName => "clear-button";
    protected override string ElementName => "span";

    [Parameter]
    public EventCallback Click { get; set; }

    protected override string GetClass()
    {
        return "s-btn-close " + Class;
    }
}