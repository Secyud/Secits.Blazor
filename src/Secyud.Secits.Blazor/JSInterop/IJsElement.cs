using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface IJsElement
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element);
}