using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// a slot for a content body.
/// content may have each way to
/// lay out or render the body.
/// </summary>
public interface IContentBodyRenderer
{
    RenderFragment? GenerateBody();
}