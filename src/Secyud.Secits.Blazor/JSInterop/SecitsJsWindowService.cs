using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SecitsJsWindowService(IJSRuntime js) : IJsWindowService
{
    public ValueTask CancelEvent(string eventName)
    {
        return js.InvokeVoidAsync("cancelEvent", eventName);
    }

    public ValueTask RestoreEvent(string eventName)
    {
        return js.InvokeVoidAsync("restoreEvent", eventName);
    }
}