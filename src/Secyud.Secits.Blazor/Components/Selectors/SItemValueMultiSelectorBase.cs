using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class SItemValueMultiSelectorBase<TComponent, TItem, TValue> :
    SItemMultiSelectorBase<TComponent, TItem>, ISchValueField<TItem, TValue>,
    ISchValues<TValue> where TComponent : ScBusinessBase
{
    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; } 

    private Func<TItem, TValue> _valueField = null!;

    [Parameter]
    public Func<IEnumerable<TValue>, Task<IEnumerable<TItem>>> ItemFinder { get; set; } = null!;

    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }

    private IEnumerable<TValue> _values = [];

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        List<Task> parameterTasks = [];

        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());

        parameters.UseParameter(_values, nameof(Values), value =>
        {
            _values = Values;
            parameterTasks.Add(SetSelectionFromParameter(value));
        });

        await base.SetParametersAsync(parameters);

        await Task.WhenAll(parameterTasks);
    }

    protected async Task SetSelectionFromParameter(IEnumerable<TValue> values)
    {
        var items = await ItemFinder.Invoke(values);
        SelectedItems = items.ToList();
    }

    protected override async Task OnItemsSelectChangedAsync(List<TItem> items)
    {
        await base.OnItemsSelectChangedAsync(items);

        Values = items.Select(u => _valueField(u)).ToList();
        if (ValuesChanged.HasDelegate)
            await ValuesChanged.InvokeAsync(Values);
    }
}