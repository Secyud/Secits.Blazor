using Microsoft.AspNetCore.Components;

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

    public override async Task ClearActiveItemAsync()
    {
        await Do(() =>
        {
            CurrentSelectedItems = [];
        });
    }

    public override bool IsItemSelected(TValue value)
    {
        return value is not null && SelectedItems.Contains(value);
    }

    public override async Task SetActiveItemAsync(TValue value)
    {
        await Do(() =>
        {
            var list = SelectedItems.ToList();
            if (!list.Remove(value)) list.Add(value);
            CurrentSelectedItems = list;
        });
    }
}