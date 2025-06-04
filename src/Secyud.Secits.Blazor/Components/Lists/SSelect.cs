using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public class SSelect<TItem, TValue> : ScSettingBase<SList<TItem>>, ISccSelect<TItem, TValue>, ISciItemSelect<TItem>
{
    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    private Func<TItem, TValue>? _valueField;


    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value?.Compile());
        
        parameters.UseParameter(Value, nameof(Value), value =>
        {
            
        });
        
        parameters.UseParameter(Values, nameof(Values), value =>
        {
            
        });

        await base.SetParametersAsync(parameters);
    }

    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }

    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = [];

    protected override void ApplySetting()
    {
        Master!.SetSelect(this);
    }

    protected override void ForgoSetting()
    {
        Master!.UnsetSelect(this);
    }

    public async Task OnItemSelectChangedAsync(TItem? item)
    {
        if (_valueField is null || !ValueChanged.HasDelegate) return;
        await ValueChanged.InvokeAsync(item is null ? default : _valueField(item));
    }

    public async Task OnItemsSelectChangedAsync(IEnumerable<TItem> items)
    {
        if (_valueField is null || !ValuesChanged.HasDelegate) return;
        await ValuesChanged.InvokeAsync(items.Select(u => _valueField(u)));
    }
}