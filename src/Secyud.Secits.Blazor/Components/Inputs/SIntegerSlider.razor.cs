using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Components;

public partial class SIntegerSlider : IScwMinMaxValue<int>
{
    private const int CacheElement = 4;

    protected override string ComponentName => "input integer-slider";

    [Parameter]
    public int Max { get; set; } = 100;

    [Parameter]
    public int Min { get; set; }

    [Parameter]
    public bool Cycle { get; set; }

    protected void OnWheel(WheelEventArgs args)
    {
        if (Readonly || Disabled) return;
        
        var value = (int)args.DeltaY / 80 + Value;
        value = Cycle ? GetCycleValue(value) : Math.Min(Math.Max(Min, value), Max);
        OnInputInvoke(value);
    }

    protected int GetCycleValue(int value)
    {
        var modValue = Max - Min + 1;
        return (value - Min + modValue) % modValue + Min;
    }

    protected override int BuildContentExtra(RenderTreeBuilder builder, int sequence)
    {
        builder.AddAttribute(sequence + 1, "onwheel", OnWheel);
        return sequence + 1;
    }
}