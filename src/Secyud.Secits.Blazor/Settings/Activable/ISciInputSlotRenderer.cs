using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciLayoutSlotRenderer
{
    RenderFragment RenderSlot();
}