using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class SItemValueSingleSelectorBase<TComponent, TItem, TValue> :
    SItemSingleSelectorBase<TComponent, TItem>, ISchValueField<TItem, TValue>,
    ISchValue<TValue> where TComponent : ScBusinessBase
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
        List<Task> parameterTasks = [];

        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());

        parameters.UseParameter(_value, nameof(Value), value =>
        {
            _value = value;
            parameterTasks.Add(SetSelectionFromParameter(value));
        });

        await base.SetParametersAsync(parameters);

        await Task.WhenAll(parameterTasks);
    }

    protected async Task SetSelectionFromParameter(TValue value)
    {
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