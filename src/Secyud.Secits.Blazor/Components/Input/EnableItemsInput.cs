using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableItemsInput<TValue> : EnableInputDelayInvokerBase<TValue>
{
    protected List<TValue> CurrentSelectedItems { get; set; } = [];

    [Parameter]
    public EventCallback<List<TValue>> SelectedItemsChanged { get; set; }

    [Parameter]
    public List<TValue> SelectedItems { get; set; } = [];

    protected override async Task OnValueChangedAsync()
    {
        if (SelectedItemsChanged.HasDelegate)
            await SelectedItemsChanged.InvokeAsync(CurrentSelectedItems);
    }

    protected override async Task OnClearActiveItemAsync(object sender)
    {
        CurrentSelectedItems = [];
        await NotifyValueChangedAsync(sender);
    }

    public override bool IsItemSelected(TValue value)
    {
        return value is not null && CurrentSelectedItems.Contains(value);
    }

    protected override async Task OnSetActiveItemAsync(object sender, TValue value)
    {
        LastActiveItem = value;
        var list = CurrentSelectedItems;
        if (!list.Remove(value)) list.Add(value);
        CurrentSelectedItems = list;
        await NotifyValueChangedAsync(sender);
    }
}