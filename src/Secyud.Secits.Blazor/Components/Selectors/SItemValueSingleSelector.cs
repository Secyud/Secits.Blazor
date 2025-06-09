using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TItem))]
public class SItemValueSingleSelector<TItem, TValue> : SItemSingleSelector<TItem>,
    ISchValueField<TItem, TValue>, ISchValue<TValue>
{
    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    private Func<TItem, TValue> _valueField = null!;

    [Parameter]
    public Func<TValue, Task<TItem>> ItemFinder { get; set; } = null!;

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    private TValue _value = default!;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());

        await base.SetParametersAsync(parameters);
        await parameters.UseParameter(_value, nameof(Value), SetSelectionFromParameter);
    }

    protected async Task SetSelectionFromParameter(TValue value)
    {
        _value = value;
        var item = await ItemFinder.Invoke(value);
        SelectedItem = item;
    }

    protected override async Task OnItemSelectChangedAsync(TItem? item)
    {
        await base.OnItemSelectChangedAsync(item);
        Value = item is null ? default! : _valueField.Invoke(item);
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(Value);
    }
}