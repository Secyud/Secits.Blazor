using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;
using Secyud.Secits.Blazor.Components;

namespace Secyud.Secits.Blazor.Abstraction;

public abstract partial class ScSelectableBase<TItem, TValue> :
    ISccSelect<TItem, TValue>, ISchTextField<TItem>, ISchValueField<TItem, TValue>
{
    private IScdSelect? _selectDelegate;

    [CascadingParameter]
    public IScdSelect? SelectDelegate
    {
        get => _selectDelegate;
        set
        {
            _selectDelegate?.UnbindComponent(this);
            _selectDelegate = value;
            _selectDelegate?.BindComponent(this);
        }
    }

    [Parameter]
    public IEnumerable<TItem>? Items { get; set; }

    [Parameter]
    public EventCallback<DataRequest> ItemsLoad { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }

    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = [];

    [Parameter]

    public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }

    [Parameter]
    public IEnumerable<TItem> SelectedItems { get; set; } = [];

    [Parameter]
    public EventCallback<TItem?> SelectedItemChanged { get; set; }

    [Parameter]
    public TItem? SelectedItem { get; set; }

    [Parameter]
    public bool MultiSelectEnabled { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField),
            u => _valueField = u?.Compile());

        await base.SetParametersAsync(parameters);
    }

    protected virtual async Task OnItemActivateChangedAsync(TItem item)
    {
        var isSelected = IsItemSelected(item);

        if (MultiSelectEnabled)
        {
            var list = SelectedItems.ToList();
            if (isSelected) list.Remove(item);
            else list.Add(item);
            await OnItemsSelectChangedAsync(list);
        }
        else
        {
            await OnItemSelectChangedAsync(isSelected ? default : item);
        }
    }

    protected virtual async Task OnItemsSelectChangedAsync(IEnumerable<TItem> items)
    {
        SelectedItems = items.ToList();

        if (SelectedItemsChanged.HasDelegate)
            await SelectedItemsChanged.InvokeAsync(SelectedItems);

        if (MultiSelectEnabled && SelectDelegate is not null)
        {
            var selectItems = SelectedItems
                .Select(u => new SelectionItem(u, GetText(u))).ToList();
            SelectDelegate.DelegateSelectItems(selectItems);
        }

        var values = SelectedItems.Select(GetValue);
        await OnValuesSelectChangedAsync(values!);
    }

    protected virtual async Task OnValuesSelectChangedAsync(IEnumerable<TValue> values)
    {
        values = values.ToList();
        Values = values;
        if (ValuesChanged.HasDelegate)
            await ValuesChanged.InvokeAsync(values);
    }

    protected virtual async Task OnItemSelectChangedAsync(TItem? item)
    {
        SelectedItem = item;

        if (SelectedItemChanged.HasDelegate)
            await SelectedItemChanged.InvokeAsync(SelectedItem);

        if (!MultiSelectEnabled && SelectDelegate is not null)
        {
            var selectItem = new SelectionItem(item, GetText(item));
            SelectDelegate.DelegateSelectItem(selectItem);
        }

        var value = GetValue(item);
        await OnValueSelectChangedAsync(value);
    }

    protected virtual async Task OnValueSelectChangedAsync(TValue? value)
    {
        Value = value!;
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);
    }

    protected virtual bool IsItemSelected(TItem? item)
    {
        return MultiSelectEnabled
            ? SelectedItems.Contains(item)
            : Equals(SelectedItem, item);
    }

    protected virtual bool IsValueSelected(TValue? value)
    {
        if (value is null) return false;
        return MultiSelectEnabled
            ? Values.Contains(value)
            : Equals(Value, value);
    }

    public virtual async Task ClearSelectAsync()
    {
        if (MultiSelectEnabled)
            await OnValuesSelectChangedAsync([]);
        else
            await OnValueSelectChangedAsync(default);
    }

    public virtual async Task UnselectObjectAsync(object obj)
    {
        if (obj is not TItem item) return;

        var value = GetValue(item);

        if (MultiSelectEnabled)
            await OnValuesSelectChangedAsync(
                Values.Where(u => !Equals(u, value)));
        else
            await OnValueSelectChangedAsync(value);
    }

    [return: NotNullIfNotNull(nameof(item))]
    protected virtual TValue? GetValue(TItem? item)
    {
        if (item is null) return default;

        if (_valueField is not null)
            return _valueField(item)!;

        return default!;
    }

    protected virtual string? GetText(TItem? item)
    {
        if (item is null) return null;

        if (TextField is not null)
            return TextField(item);

        return item.ToString();
    }

    [Parameter]
    public Func<TItem, string?>? TextField { get; set; }

    private Func<TItem, TValue>? _valueField;

    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }
}