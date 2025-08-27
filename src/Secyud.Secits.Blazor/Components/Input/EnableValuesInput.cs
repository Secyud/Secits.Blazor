using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableValuesInput<TValue, TItemValue> : EnableItemsInput<TValue>, IHasValues<TItemValue>
{
    private Func<TValue, TItemValue>? _valueField;

    [Parameter]
    public Expression<Func<TValue, TItemValue>>? ValueField { get; set; }

    [Parameter]
    public Func<IEnumerable<TItemValue>, Task<IEnumerable<TValue>>>? ItemFinder { get; set; }

    [Parameter]
    public List<TItemValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<List<TItemValue>> ValuesChanged { get; set; }

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        base.BeforeParametersSet(parameters);
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());
        parameters.UseParameter(Values, nameof(Values), TrySetSelectionFromParameter);
    }

    protected async Task TrySetSelectionFromParameter(List<TItemValue> values)
    {
        if (ItemFinder is not null)
        {
            var items = await ItemFinder.Invoke(values);
            CurrentValues = items.ToList();
            return;
        }

        if (typeof(TValue).IsAssignableFrom(typeof(TItemValue)))
            CurrentValues = values.Cast<TValue>().ToList();
        else
            throw new InvalidOperationException(
                $"Please set {nameof(ItemFinder)} in {nameof(EnableValueInput<TValue, TItemValue>)}.");
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        if (ValuesChanged.HasDelegate)
        {
            var values = CurrentValues.Select(GetValue).ToList();
            await ValuesChanged.InvokeAsync(values);
        }
    }


    protected TItemValue GetValue(TValue item)
    {
        if (_valueField is null)
        {
            if (item is TItemValue value)
                return value;
        }
        else
        {
            return _valueField(item);
        }

        throw new InvalidOperationException("Please set ValueField in EnableValuesInput.");
    }
}