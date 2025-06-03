using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class ScSelectionBase<TValue> : ISccSelect<TValue>, ISciSelect
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

    #region Parameter

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }

    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = [];

    [Parameter]
    public bool MultiSelectEnabled { get; set; }

    [Parameter]
    public string? Format { get; set; }

    #endregion

    #region SelectEvent

    protected virtual async Task OnValuesSelectChangedAsync(IEnumerable<TValue> values)
    {
        values = values.ToList();
        Values = values;
        if (ValuesChanged.HasDelegate)
            await ValuesChanged.InvokeAsync(values);
        _selectDelegate?.OnDelegateSelectItemsAsync(values
            .Select(u => SelectionItem.FromObject(u, Format)));
    }

    protected virtual async Task OnValueSelectChangedAsync(TValue? value)
    {
        Value = value!;
        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);
        _selectDelegate?.OnDelegateSelectItemAsync(SelectionItem.FromObject(value, Format));
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
        if (obj is not TValue value) return;

        if (MultiSelectEnabled)
            await OnValuesSelectChangedAsync(
                Values.Where(u => !Equals(u, value)));
        else
            await OnValueSelectChangedAsync(value);
    }

    #endregion
}