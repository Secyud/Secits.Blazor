using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsGlobalEvents
{
    event Action<MouseEventArgs> MouseMoveEvent;
    event Action<MouseEventArgs> MouseDownEvent;
    event Action<MouseEventArgs> MouseUpEvent;
}