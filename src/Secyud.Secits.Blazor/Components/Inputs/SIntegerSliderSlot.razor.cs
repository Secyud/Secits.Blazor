using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Components;

public partial class SIntegerSliderSlot : IScwMinMaxValue<int>, ISciInputSlotRenderer<int>
{
    private const int CacheElement = 4;

    [Parameter]
    public int Max { get; set; } = 100;

    [Parameter]
    public int Min { get; set; }

    [Parameter]
    public bool Cycle { get; set; }

    [Parameter]
    public string? Format { get; set; }

    private async Task OnWheelAsync(WheelEventArgs args)
    {
        if (Master is null || Master.Readonly || Master.Disabled) return;

        var value = (int)args.DeltaY / 80 + Master.Value;
        value = Cycle ? GetCycleValue(value) : Math.Min(Math.Max(Min, value), Max);
        
        await Master.OnValueChangedAsync(value);
    }

    private int GetCycleValue(int value)
    {
        var modValue = Max - Min + 1;
        return (value - Min + modValue) % modValue + Min;
    }

    public Task SetValueFromParameterAsync(int value)
    {
        return Task.CompletedTask;
    }

    #region Render

    private RenderFragment GenerateNumberBox() => builder =>
    {
        var currentValue = Master!.Value;
        for (var offset = -CacheElement; offset < CacheElement; offset++)
        {
            var value = currentValue + offset;

            if (Cycle)
                value = GetCycleValue(value);
            else if ((offset < 0 && currentValue < Min - offset) ||
                     (offset > 0 && currentValue > Max - offset))
                continue;
            builder.AddContent(value, GenerateOffset(offset, value));
        }
    };

    #endregion
}