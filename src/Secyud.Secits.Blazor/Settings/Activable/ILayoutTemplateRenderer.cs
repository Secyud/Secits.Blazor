using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the common slot.
/// TODO add layout parameter.
/// 
/// </summary>
public interface ILayoutTemplateRenderer : IIsSetting
{
    RenderFragment RenderTemplate();
}