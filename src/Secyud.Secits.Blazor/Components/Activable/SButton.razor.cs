using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class SButton : ICanClick
{
    protected override string ComponentName => "button";

    [Parameter]
    public EventCallback Click { get; set; }

    protected virtual void OnClick()
    {
        Click.InvokeAsync();
    }
}