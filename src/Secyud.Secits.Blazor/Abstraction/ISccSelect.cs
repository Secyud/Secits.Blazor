using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Abstraction;

public interface ISccSelect
{
    IScdSelect? SelectDelegate { get; set; }
    Task UnselectObjectAsync(object obj);
    Task ClearSelectAsync();
}

public interface ISccSelect<TItem, TValue> :
    IScwItems<TItem>,
    ISchValue<TValue>,
    ISccSelect
{
    EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }
    IEnumerable<TValue> Values { get; set; }
    EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }
    IEnumerable<TItem> SelectedItems { get; set; }
    EventCallback<TItem?> SelectedItemChanged { get; set; }
    TItem? SelectedItem { get; set; }
    bool MultiSelectEnabled { get; set; }
}