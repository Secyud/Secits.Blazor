using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISchValues<TValue>
{
    IEnumerable<TValue> Values { get; set; }

    EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }
}