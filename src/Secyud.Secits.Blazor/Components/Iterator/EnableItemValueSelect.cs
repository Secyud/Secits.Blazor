using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public class EnableItemValueSelect<TItem, TValue> : EnableItemSelect<TItem>,
    IHasValueField<TItem, TValue>, IHasValue<TValue>
{
    private Func<TItem, TValue> _valueField = null!;

    [Parameter]
    public Expression<Func<TItem, TValue>>? ValueField { get; set; }

    [Parameter]
    public Func<TValue, Task<TItem>> ItemFinder { get; set; } = null!;


    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        parameters.UseParameter(ValueField, nameof(ValueField), value => _valueField = value!.Compile());
        parameters.UseParameter(Value, nameof(Value), SetSelectionFromParameter);
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