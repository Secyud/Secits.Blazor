using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface IValueComponent<TValue>
{
    TValue Value { get; set; }

    EventCallback<TValue> ValueChanged { get; set; }
}