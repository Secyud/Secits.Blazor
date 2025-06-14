using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SButton : ISccClick
{
    protected override string ComponentName => "button";

    [Parameter]
    public EventCallback Click { get; set; }

    protected virtual void OnClick()
    {
        Click.InvokeAsync();
    }
}