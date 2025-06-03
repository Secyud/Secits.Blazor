using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SecitsJsGlobalEvents(IJSRuntime jsRuntime):IJsGlobalEvents
{
    public event Action<MouseEventArgs>? MouseMoveEvent;
    public event Action<MouseEventArgs>? MouseDownEvent;
    public event Action<MouseEventArgs>? MouseUpEvent;
}