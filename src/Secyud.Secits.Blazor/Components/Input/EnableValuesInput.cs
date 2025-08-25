using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableValuesInput<TValue, TItemValue> : EnableItemsInput<TValue>, IHasValues<TItemValue>
{
    private Func<TValue, TItemValue> _valueField = null!;

    [Parameter]
    public Expression<Func<TValue, TItemValue>>? ValueField { get; set; }

    [Parameter]
    public Func<IEnumerable<TItemValue>, Task<IEnumerable<TValue>>> ItemFinder { get; set; } = null!;

    [Parameter]
    public List<TItemValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<List<TItemValue>> ValuesChanged { get; set; }

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        base.BeforeParametersSet(parameters);
        parameters.UseParameter(ValueField, nameof(ValueField),
            value => _valueField = value!.Compile());
        parameters.UseParameter(Values, nameof(Values), TrySetSelectionFromParameter);
    }

    protected async Task TrySetSelectionFromParameter(List<TItemValue> values)
    {
        var items = await ItemFinder.Invoke(values);
        CurrentSelectedItems = items.ToList();
    }

    protected override async Task OnValueChangedAsync()
    {
        await base.OnValueChangedAsync();
        if (ValuesChanged.HasDelegate)
        {
            var values = CurrentSelectedItems
                .Select(u => _valueField(u)).ToList();
            await ValuesChanged.InvokeAsync(values);
        }
    }
}