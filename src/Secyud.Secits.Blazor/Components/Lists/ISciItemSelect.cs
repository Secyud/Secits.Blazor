namespace Secyud.Secits.Blazor.Components;

public interface ISciItemSelect<in TItem>
{
    Task OnItemSelectChangedAsync(TItem? item);
    Task OnItemsSelectChangedAsync(IEnumerable<TItem> items);
}