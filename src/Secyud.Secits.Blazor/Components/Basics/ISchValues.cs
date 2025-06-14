using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISchValues<TValue>
{
    List<TValue> Values { get; set; }

    EventCallback<List<TValue>> ValuesChanged { get; set; }
}