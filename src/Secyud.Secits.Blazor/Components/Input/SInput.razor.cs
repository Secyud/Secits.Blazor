using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInput<TValue>
{
    protected override string ComponentName => "input";


    #region Parameters

    private TValue _value = default!;

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    #endregion

    public TValue CurrentValue { get; protected set; } = default!;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        await parameters.UseParameter(_value, nameof(Value), async value =>
        {
            _value = value;
            CurrentValue = value;
            await SetValueFromParameterAsync(value);
        });
    }

    #region Settings

    public SSetting<IInputInvoker<TValue>> InputInvoker { get; } = new();

    public virtual async Task OnValueChangedAsync(TValue value)
    {
        CurrentValue = value;
        await InputInvoker.InvokeAsync(
            u => u.InvokeValueChanged(this, value),
            () => ValueChanged.InvokeAsync(value));
    }

    public SSettings<IValueContainer<TValue>> ValueContainer { get; } = new();

    private async Task SetValueFromParameterAsync(TValue value)
    {
        await ValueContainer.InvokeAsync(u => u.SetValueFromParameterAsync(value));
    }

    public SSetting<IInputValueConverter<TValue>> ValueConverter { get; } = new();

    #endregion
}