using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

public partial class CloseButton : ISccClick
{
    [Parameter]
    public EventCallback Click { get; set; }

    protected virtual void OnClick()
    {
        Click.InvokeAsync();
    }
}