using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableValueInput<TValue> : EnableInputDelayInvokerBase<TValue>, IHasCurrentValue<TValue>, IHasValue<TValue>
{
    public TValue CurrentValue
    {
        get => LastActiveItem;
        set => LastActiveItem = value;
    }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }


    protected override async Task OnValueChangedAsync()
    {
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(CurrentValue);
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