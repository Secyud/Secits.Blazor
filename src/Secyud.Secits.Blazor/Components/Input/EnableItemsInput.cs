using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableItemsInput<TValue> : EnableInputDelayInvokerBase<TValue>, IHasCurrentValues<TValue>
{
    public List<TValue> CurrentValues { get; set; } = [];

    [Parameter]
    public EventCallback<List<TValue>> SelectedItemsChanged { get; set; }

    [Parameter]
    public List<TValue> SelectedItems { get; set; } = [];

    protected override async Task OnValueChangedAsync()
    {
        if (SelectedItemsChanged.HasDelegate)
            await SelectedItemsChanged.InvokeAsync(CurrentValues);
    }

    protected override async Task OnClearActiveItemAsync(object sender)
    {
        CurrentValues = [];
        await NotifyValueChangedAsync(sender);
    }

    public override bool IsItemSelected(TValue value)
    {
        return value is not null && CurrentValues.Contains(value);
    }

    protected override async Task OnSetActiveItemAsync(object sender, TValue value)
    {
        LastActiveItem = value;
        var list = CurrentValues;
        if (!list.Remove(value)) list.Add(value);
        CurrentValues = list;
        await NotifyValueChangedAsync(sender);
    }
}