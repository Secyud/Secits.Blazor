using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISccSelect<TValue> : ISchValue<TValue>
{
    EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }
    IEnumerable<TValue> Values { get; set; }
}

public interface ISccSelect<TItem, TValue> : ISccSelect<TValue>, ISchValueField<TItem, TValue>
{
}