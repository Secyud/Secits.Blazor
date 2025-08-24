using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SMultipleInput<TValue> : IHasValues<TValue>, IInputInvokable<List<TValue>>
{
    #region Parameters

    [Parameter]
    public List<TValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<List<TValue>> ValuesChanged { get; set; }

    private TValue _cachedValue = default!;

    #endregion

    public List<TValue> CurrentValue { get; protected set; } = [];

    public override async Task SetSingleValue(TValue value)
    {
        _cachedValue = value;
        CurrentValue.Add(value);
        await InputInvoker.InvokeAsync(
            u => u.InvokeValueChanged(this, CurrentValue),
            () => ValuesChanged.InvokeAsync(CurrentValue));
    }

    public override TValue GetSingleValue()
    {
        return _cachedValue;
    }

    public override Task OnValueCleared()
    {
        CurrentValue = [];
        return Task.CompletedTask;
    }

    public SSettings<IValueContainer<List<TValue>>> ValuesContainer { get; } = new();

    protected override void BeforeParametersSet(ParameterContainer parameters)
    {
        parameters.UseParameter(Values, nameof(Values), async values =>
        {
            CurrentValue = values;
            await ValuesContainer.InvokeAsync(u => u.SetValueFromParameterAsync(values));
        });
    }

    public SSetting<IInputInvoker<List<TValue>>> InputInvoker { get; } = new();

    public async Task OnValueChangedAsync(List<TValue> value)
    {
        if (ValuesChanged.HasDelegate)
            await ValuesChanged.InvokeAsync(value);
    }
}