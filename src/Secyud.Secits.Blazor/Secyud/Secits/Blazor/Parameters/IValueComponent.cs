using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Parameters;

public interface IValueComponent<TValue>
{
    TValue Value { get; set; }

    EventCallback<TValue> ValueChanged { get; set; }
}