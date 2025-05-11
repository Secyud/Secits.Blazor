using System.Numerics;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SInputSlider<TValue> : SInputBase<TValue>, IMinMaxValueComponent<TValue>
    where TValue : struct, INumber<TValue>
{
    protected override string InputType => "range";

    [Parameter]
    public TValue Max { get; set; }

    [Parameter]
    public TValue Min { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        TrySetAttribute<TValue>(parameters, nameof(Min), "min");
        TrySetAttribute<TValue>(parameters, nameof(Max), "max");
    }
}