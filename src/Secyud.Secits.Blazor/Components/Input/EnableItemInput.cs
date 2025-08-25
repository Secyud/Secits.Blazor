using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableItemInput<TValue> : EnableInputDelayInvokerBase<TValue>
{
    protected TValue CurrentSelectedItem
    {
        get => LastActiveItem;
        set => LastActiveItem = value;
    }

    [Parameter]
    public EventCallback<TValue> SelectedItemChanged { get; set; }

    [Parameter]
    public TValue SelectedItem { get; set; } = default!;


    protected override async Task OnValueChangedAsync()
    {
        if (SelectedItemChanged.HasDelegate)
            await SelectedItemChanged.InvokeAsync(CurrentSelectedItem);
    }

    public override bool IsItemSelected(TValue value)
    {
        return Equals(CurrentSelectedItem, value);
    }

    public override async Task ClearActiveItemAsync()
    {
        await Do(() => { CurrentSelectedItem = default!; });
    }

    public override async Task SetActiveItemAsync(TValue value)
    {
        await Do(() =>
        {
            CurrentSelectedItem = Equals(CurrentSelectedItem, value) ? default! : value;
        });
        await InvokeAsync(StateHasChanged);
    }
}