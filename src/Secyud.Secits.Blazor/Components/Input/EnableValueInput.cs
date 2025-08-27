using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableValueInput<TValue, TItemValue> : EnableItemInput<TValue>, IHasValue<TItemValue>
{
    private Func<TValue, TItemValue>? _valueField;

    [Parameter]
    public Expression<Func<TValue, TItemValue>>? ValueField { get; set; }

    [Parameter]
    public Func<TItemValue, Task<TValue>>? ItemFinder { get; set; }

    [Parameter]
    public TItemValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TItemValue> ValueChanged { get; set; }

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        base.BeforeParametersSet(parameters);
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value?.Compile());
        parameters.UseParameter(Value, nameof(Value), SetSelectionFromParameter);
    }

    protected async Task SetSelectionFromParameter(TItemValue value)
    {
        if (ItemFinder is not null)
        {
            CurrentValue = await ItemFinder.Invoke(value);
            return;
        }

        if (value is TValue item)
            CurrentValue = item;
        else
            throw new InvalidOperationException(
                $"Please set {nameof(ItemFinder)} in {nameof(EnableValueInput<TValue, TItemValue>)}.");
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        if (ValueChanged.HasDelegate)
        {
            var value = CurrentValue is null ? default! : GetValue(CurrentValue);
            await ValueChanged.InvokeAsync(value);
        }
    }

    protected TItemValue GetValue(TValue item)
    {
        if (_valueField is not null)
            return _valueField(item);

        if (item is TItemValue value) return value;

        throw new InvalidOperationException(
            $"Please set {nameof(ValueField)} in {nameof(EnableValueInput<TValue, TItemValue>)}.");
    }
}