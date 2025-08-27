using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableItemInput<TValue> : EnableInputDelayInvokerBase<TValue>,IHasCurrentValue<TValue>
{
    public TValue CurrentValue
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
            await SelectedItemChanged.InvokeAsync(CurrentValue);
    }

    public override bool IsItemSelected(TValue value)
    {
        return Equals(CurrentValue, value);
    }

    protected override async Task OnClearActiveItemAsync(object sender)
    {
        CurrentValue = default!;
        await NotifyValueChangedAsync(sender);
    }

    protected override async Task OnSetActiveItemAsync(object sender, TValue value)
    {
        CurrentValue = value;
        await NotifyValueChangedAsync(sender);
    }
}