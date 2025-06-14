using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public partial class SIntegerSliderSlot : IScwMinMaxValue<int>, ISciValueContainer<int>
{
    [Parameter, Range(1, 4)]
    public int NumberCount { get; set; } = 1;

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
        if (Master.Readonly || Master.Disabled) return;

        var value = (int)args.DeltaY / 80 + Master.Value;
        value = Cycle ? GetCycleValue(value) : Math.Min(Math.Max(Min, value), Max);

        await Master.OnValueChangedAsync(value);
    }

    private int GetCycleValue(int value)
    {
        var modValue = Max - Min + 1;
        return (value - Min + modValue) % modValue + Min;
    }

    #region Render

    private RenderFragment GenerateNumberBox() => builder =>
    {
        var currentValue = Master.Value;
        var cacheElement = NumberCount * 2;
        for (var offset = -cacheElement; offset <= cacheElement; offset++)
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

    protected override void ApplySetting()
    {
        Master.ValueContainer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ValueContainer.Forgo(this);
    }
}