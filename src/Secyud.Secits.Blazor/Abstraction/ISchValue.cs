using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Abstraction;

public interface ISchValue<TValue>
{
    TValue Value { get; set; }

    EventCallback<TValue> ValueChanged { get; set; }
}