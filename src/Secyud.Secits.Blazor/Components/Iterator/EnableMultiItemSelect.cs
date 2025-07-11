﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public class EnableMultiItemSelect<TItem> : EnableSelectBase<SIteratorBase<TItem>, TItem>, IRowRenderer<TItem>
{
    [Parameter]
    public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }

    [Parameter]
    public List<TItem> SelectedItems { get; set; } = [];

    [Parameter]
    public RenderFragment<SelectionsContext<TItem>>? SelectContent { get; set; }

    public override RenderFragment? GenerateSelectedContent() =>
        SelectContent?.Invoke(new SelectionsContext<TItem>(this, SelectedItems));

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
        if (item is null) return false;
        return SelectedItems.Contains(item);
    }

    public override async Task OnSelectionActivateAsync(TItem item)
    {
        var list = SelectedItems.ToList();
        if (!list.Remove(item)) list.Add(item);
        await OnItemsSelectChangedAsync(list);
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
}