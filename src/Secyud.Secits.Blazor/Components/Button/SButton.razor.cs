using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SButton : IClickComponent,IColorComponent
{
    protected override string ComponentName => "btn";
    protected override string ElementName => "button";

    [Parameter]
    public EventCallback Click { get; set; }

    [Parameter]
    public ColorType Color { get; set; }
}