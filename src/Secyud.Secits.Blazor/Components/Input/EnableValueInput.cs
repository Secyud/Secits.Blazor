using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableValueInput<TValue, TItemValue> : EnableItemInput<TValue>, IHasValue<TItemValue>
{
    private Func<TValue, TItemValue> _valueField = null!;

    [Parameter]
    public Expression<Func<TValue, TItemValue>>? ValueField { get; set; }

    [Parameter]
    public Func<TItemValue, Task<TValue>> ItemFinder { get; set; } = null!;

    [Parameter]
    public TItemValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TItemValue> ValueChanged { get; set; }

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        base.BeforeParametersSet(parameters);
        parameters.UseParameter(ValueField, nameof(ValueField), value => _valueField = value!.Compile());
        parameters.UseParameter(Value, nameof(Value), SetSelectionFromParameter);
    }

    protected async Task SetSelectionFromParameter(TItemValue value)
    {
        var item = await ItemFinder.Invoke(value);
        CurrentSelectedItem = item;
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        if (ValueChanged.HasDelegate)
        {
            var value = CurrentSelectedItem is null
                ? default!
                : _valueField.Invoke(CurrentSelectedItem);
            await ValueChanged.InvokeAsync(value);
        }
    }
}