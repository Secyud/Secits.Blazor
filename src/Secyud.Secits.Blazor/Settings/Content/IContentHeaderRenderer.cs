using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// render the header slot of a content.
/// </summary>
public interface IContentHeaderRenderer : IIsSetting
{
    RenderFragment? GenerateHeader();
}