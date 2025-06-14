using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor;

public class SJsElement(IJSRuntime js) : JsElementBase(js), IJsElement
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element)
    {
        return InvokeAsync<DomRect>(element, "getBoundingClientRect");
    }
}