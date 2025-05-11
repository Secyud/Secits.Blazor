using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class CloseButton : IClickComponent
{
    protected override string ComponentName => "clear-button";
    protected override string ElementName => "span";

    [Parameter]
    public EventCallback Click { get; set; }
}