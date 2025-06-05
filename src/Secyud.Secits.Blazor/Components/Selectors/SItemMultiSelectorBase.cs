using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class SItemMultiSelectorBase<TComponent, TItem> : SSelectorBase<TComponent, TItem>
    where TComponent : ScBusinessBase
{
    [Parameter]
    public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }

    [Parameter]
    public IEnumerable<TItem> SelectedItems { get; set; } = [];

    protected virtual async Task OnItemsSelectChangedAsync(List<TItem> items)
    {
        SelectedItems = items.ToList();

        if (SelectedItemsChanged.HasDelegate)
            await SelectedItemsChanged.InvokeAsync(SelectedItems);
    }

    public override async Task ClearSelectAsync()
    {
        await OnItemsSelectChangedAsync([]);
    }

    public override bool IsSelected(TItem? item)
    {
        return SelectedItems.Contains(item);
    }

    public override async Task OnSelectionActivateAsync(TItem item)
    {
        var list = SelectedItems.ToList();
        if (!list.Remove(item)) list.Add(item);
        await OnItemsSelectChangedAsync(list);
    }
}