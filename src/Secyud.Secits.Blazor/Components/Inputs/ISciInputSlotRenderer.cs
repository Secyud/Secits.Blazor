using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciInputSlotRenderer<in TValue>
{
    RenderFragment RenderSlot();

    public Task SetValueFromParameterAsync(TValue value)
    {
        return Task.CompletedTask;
    }
}