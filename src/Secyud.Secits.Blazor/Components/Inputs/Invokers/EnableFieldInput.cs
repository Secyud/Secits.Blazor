using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class EnableFieldInput<TValue, TField> : EnableValueInput<TValue>
{
    private Func<TValue, TField>? _valueField;

    [Parameter]
    public Expression<Func<TValue, TField>>? ValueField { get; set; }

    [Parameter]
    public Func<TField, Task<TValue>>? ItemFinder { get; set; }

    [Parameter]
    public TField Field { get; set; } = default!;

    [Parameter]
    public EventCallback<TField> FieldChanged { get; set; }

    [Parameter]
    public Expression<Func<TField>>? FieldExpression { get; set; }

    [Parameter]
    public Func<TField, List<ValidationResult>>? FieldValidator { get; set; }

    protected override void PreParametersSet(ParameterContainer parameters)
    {
        base.PreParametersSet(parameters);
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value?.Compile());
        parameters.UseParameter(Field, nameof(Field), SetSelectionFromParameter);
    }

    protected async Task SetSelectionFromParameter(TField value)
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
                $"Please set {nameof(ItemFinder)} in {nameof(EnableFieldInput<TValue, TField>)}.");
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        var value = CurrentValue is null ? default! : GetValue(CurrentValue);
        if (FieldChanged.HasDelegate)
        {
            await FieldChanged.InvokeAsync(value);
            await ChangeValidationAsync(value, FieldExpression, FieldValidator);
        }
    }

    protected TField GetValue(TValue item)
    {
        if (_valueField is not null)
            return _valueField(item);

        if (item is TField value) return value;

        throw new InvalidOperationException(
            $"Please set {nameof(ValueField)} in {nameof(EnableFieldInput<TValue, TField>)}.");
    }
}