using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public interface ISciSelect
{
    RenderFragment? GenerateSelectedContent();
    Task ClearSelectAsync();
}