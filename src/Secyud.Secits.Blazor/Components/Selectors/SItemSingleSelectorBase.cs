using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public abstract class SItemSingleSelectorBase<TComponent, TItem> : SSelectorBase<TComponent, TItem>
    where TComponent : ScBusinessBase
{
    [Parameter]
    public EventCallback<TItem?> SelectedItemChanged { get; set; }

    [Parameter]
    public TItem? SelectedItem { get; set; }

    protected virtual async Task OnItemSelectChangedAsync(TItem? item)
    {
        SelectedItem = item;

        if (SelectedItemChanged.HasDelegate)
            await SelectedItemChanged.InvokeAsync(SelectedItem);
    }

    public override async Task ClearSelectAsync()
    {
        await OnItemSelectChangedAsync(default);
    }

    protected override bool IsSelected(TItem? item)
    {
        return Equals(SelectedItem, item);
    }

    protected override async Task OnSelectionActivateAsync(TItem item)
    {
        await OnItemSelectChangedAsync(Equals(item, SelectedItem) ? default : item);
    }
}