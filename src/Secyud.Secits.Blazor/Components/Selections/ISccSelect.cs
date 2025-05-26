using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISccSelect
{
    IScdSelect? SelectDelegate { get; set; }
    Task UnselectObjectAsync(object obj);
    Task ClearSelectAsync();
    bool MultiSelectEnabled { get; set; }
}

public interface ISccSelect<TValue> : ISccSelect,
    ISchValue<TValue>
{
    EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }
    IEnumerable<TValue> Values { get; set; }
}

public interface ISccSelect<TItem, TValue> :
    ISccSelect<TValue>
{
    EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }
    IEnumerable<TItem> SelectedItems { get; set; }
    EventCallback<TItem?> SelectedItemChanged { get; set; }
    TItem? SelectedItem { get; set; }
}