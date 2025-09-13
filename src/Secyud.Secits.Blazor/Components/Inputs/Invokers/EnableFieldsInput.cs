using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class EnableFieldsInput<TValue, TField> : EnableValuesInput<TValue>
{
    private Func<TValue, TField>? _valueField;

    [Parameter]
    public Expression<Func<TValue, TField>>? ValueField { get; set; }

    [Parameter]
    public Func<IEnumerable<TField>, Task<IEnumerable<TValue>>>? ItemFinder { get; set; }

    [Parameter]
    public List<TField> Fields { get; set; } = [];

    [Parameter]
    public EventCallback<List<TField>> FieldsChanged { get; set; }

    [Parameter]
    public Expression<Func<List<TField>>>? FieldsExpression { get; set; }

    protected override void PreParametersSet(ParameterContainer parameters)
    {
        base.PreParametersSet(parameters);
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());
        parameters.UseParameter(Fields, nameof(Fields), TrySetSelectionFromParameter);
    }

    protected async Task TrySetSelectionFromParameter(List<TField> values)
    {
        if (ItemFinder is not null)
        {
            var items = await ItemFinder.Invoke(values);
            CurrentValues = items.ToList();
            return;
        }

        if (typeof(TValue).IsAssignableFrom(typeof(TField)))
            CurrentValues = values.Cast<TValue>().ToList();
        else
            throw new InvalidOperationException(
                $"Please set {nameof(ItemFinder)} in {nameof(EnableFieldInput<TValue, TField>)}.");
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        if (FieldsChanged.HasDelegate)
        {
            var values = CurrentValues.Select(GetValue).ToList();
            await FieldsChanged.InvokeAsync(values);
        }
    }


    protected TField GetValue(TValue item)
    {
        if (_valueField is null)
        {
            if (item is TField value)
                return value;
        }
        else
        {
            return _valueField(item);
        }

        throw new InvalidOperationException("Please set ValueField in EnableValuesInput.");
    }
}