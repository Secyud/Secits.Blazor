using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class InputSliderBoxTemplate : IHasRange<int>
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

    protected int GetValue()
    {
        if (Master.InputInvoker.Get() is { } invoker)
        {
            return invoker.GetActiveItem();
        }

        return 0;
    }

    protected async Task SetValueAsync(int value)
    {
        if (Master.InputInvoker.Get() is { } invoker)
        {
            await invoker.SetActiveItemAsync(this, value);
        }
    }

    public override RendererPosition GetLayoutPosition()
    {
        return RendererPosition.Body;
    }
}