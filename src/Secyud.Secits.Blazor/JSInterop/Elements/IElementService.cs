using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.JSInterop;

public interface IElementService
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element);
}