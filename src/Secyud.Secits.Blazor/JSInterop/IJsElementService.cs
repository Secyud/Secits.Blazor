using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsElementService
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element);
}