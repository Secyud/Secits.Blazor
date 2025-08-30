using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SliderBox : ICanActive, IHasRange<int>
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

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public EventCallback<int> ValueChanged { get; set; }

    private async Task OnWheelAsync(WheelEventArgs args)
    {
        if (Readonly || Disabled) return;

        var value = (int)args.DeltaY / 80 + Value;
        value = Cycle ? GetCycleValue(value) : Math.Min(Math.Max(Min, value), Max);

        await OnValueChangedAsync(value);
    }

    private async Task OnValueChangedAsync(int value)
    {
        Value = value;

        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);

        await InvokeAsync(StateHasChanged);
    }

    private int GetCycleValue(int value)
    {
        var modValue = Max - Min + 1;
        return (value - Min + modValue) % modValue + Min;
    }

    #region Render

    #endregion
}