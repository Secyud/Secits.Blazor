using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

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

    protected override async Task OnClearActiveItemAsync(object sender)
    {
        CurrentSelectedItem = default!;
        await NotifyValueChangedAsync(sender);
    }

    protected override async Task OnSetActiveItemAsync(object sender, TValue value)
    {
        CurrentSelectedItem = Equals(CurrentSelectedItem, value) ? default! : value;
        await NotifyValueChangedAsync(sender);
    }
}