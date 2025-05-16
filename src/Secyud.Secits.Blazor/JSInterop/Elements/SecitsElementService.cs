using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SecitsElementService(IJSRuntime js) :JsElement(js), IElementService
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element)
    {
        return InvokeAsync<DomRect>(element, "getBoundingClientRect");
    }
}