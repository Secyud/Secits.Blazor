using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TItem))]
public class SItemSingleSelector<TItem> : SSelectorBase<SItemsIteratorBase<TItem>, TItem>,
    ISciRowRenderer<TItem>
{
    [Parameter]
    public EventCallback<TItem?> SelectedItemChanged { get; set; }

    [Parameter]
    public TItem? SelectedItem { get; set; }

    [Parameter]
    public RenderFragment<SSingleSelectorContext<TItem>>? SelectContent { get; set; }

    public override RenderFragment? GenerateSelectedContent() =>
        SelectContent?.Invoke(new(this, SelectedItem));

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

    public override bool IsSelected(TItem? item)
    {
        return Equals(SelectedItem, item);
    }

    public override async Task OnSelectionActivateAsync(TItem item)
    {
        await OnItemSelectChangedAsync(Equals(item, SelectedItem) ? default : item);
        await Master.RefreshUiAsync();
    }

    protected override void ApplySetting()
    {
        Master.RowRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.RowRenderer.Forgo(this);
    }

    public virtual string? GetRowClass(TItem item)
    {
        return IsSelected(item) ? "selected" : null;
    }

    public virtual string? GetRowStyle(TItem item)
    {
        return null;
    }

    public virtual void OnRowClick(MouseEventArgs args, TItem item)
    {
        OnSelectionActivateAsync(item).ConfigureAwait(false);
    }

    public bool ClickEnabled => true;
}