using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface ISciSelect
{
    RenderFragment? GenerateSelectedContent();
    Task ClearSelectAsync();
}