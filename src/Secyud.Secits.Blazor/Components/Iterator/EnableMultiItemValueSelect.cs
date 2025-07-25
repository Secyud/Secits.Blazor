﻿using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public class EnableMultiItemValueSelect<TItem, TValue> : EnableMultiItemSelect<TItem>,
    IHasValueField<TItem, TValue>, IHasValues<TValue>
{
    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    private Func<TItem, TValue> _valueField = null!;

    [Parameter]
    public Func<IEnumerable<TValue>, Task<IEnumerable<TItem>>> ItemFinder { get; set; } = null!;

    [Parameter]
    public List<TValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<List<TValue>> ValuesChanged { get; set; }

    private List<TValue> _values = [];

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());
        await base.SetParametersAsync(parameters);
        await parameters.UseParameter(_values, nameof(Values), SetSelectionFromParameter);
    }

    protected async Task SetSelectionFromParameter(List<TValue> values)
    {
        _values = values;
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