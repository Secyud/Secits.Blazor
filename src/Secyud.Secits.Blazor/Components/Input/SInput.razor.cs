using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInput<TValue>
{
    protected override string ComponentName => "input";


    private TValue _value = default!;

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

    public SSetting<ISciInputInvoker<TValue>> InputInvoker { get; } = new();

    public override async Task OnValueChangedAsync(TValue value)
    {
        CurrentValue = value;
        await InputInvoker.InvokeAsync(
            u => u.InvokeValueChanged(this, value),
            () => ValueChanged.InvokeAsync(value));
    }

    public SSettings<ISciValueContainer<TValue>> ValueContainer { get; } = new();

    private async Task SetValueFromParameterAsync(TValue value)
    {
        await ValueContainer.InvokeAsync(u => u.SetValueFromParameterAsync(value));
    }

    public SSetting<ISciInputValueConverter<TValue>> ValueConverter { get; } = new();

    #endregion
}