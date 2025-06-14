using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// render the footer slot of a content.
/// </summary>
public interface IContentFooterRenderer
{
    RenderFragment? GenerateFooter();
}