using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciInputSlotRenderer<in TValue>
{
    RenderFragment RenderSlot();

    Task SetValueFromParameterAsync(TValue value);
}