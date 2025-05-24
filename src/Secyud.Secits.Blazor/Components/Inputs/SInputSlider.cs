using System.Numerics;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Abstraction;

namespace Secyud.Secits.Blazor.Components;

public class SInputSlider<TValue> : SInputBase<TValue>, IScwMinMaxValue<TValue>
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

        TrySetAttribute<TValue>(parameters, "min");
        TrySetAttribute<TValue>(parameters, "max");
    }
}