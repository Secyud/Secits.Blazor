using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SecitsJsElementServiceBaseService(IJSRuntime js) : JsElementServiceBase(js), IJsElementService
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element)
    {
        return InvokeAsync<DomRect>(element, "getBoundingClientRect");
    }
}