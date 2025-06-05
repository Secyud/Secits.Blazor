using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInput<TValue>
{
    protected override string ComponentName => "input";


    private TValue _value = default!;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        var tasks = new List<Task>();
        parameters.UseParameter(_value, nameof(Value), value =>
        {
            _value = value;
            CurrentValue = value;
            tasks.Add(SetValueFromParameterAsync(value));
        });
        await base.SetParametersAsync(parameters);
        await Task.WhenAll(tasks);
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

    public SSettings<ISciInputSlotRenderer<TValue>> InputSlotRenderers { get; } = new();

    private async Task SetValueFromParameterAsync(TValue value)
    {
        await InputSlotRenderers.InvokeAsync(u => u.SetValueFromParameterAsync(value));
    }

    public SSetting<ISciInputValueConverter<TValue>> ValueConverter { get; } = new();

    #endregion
}