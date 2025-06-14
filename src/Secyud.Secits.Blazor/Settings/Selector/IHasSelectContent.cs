using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// this is a special setting.
/// it is not use for component. it only attached
/// to the selection displayer. the selection
/// displayer can present the selected items
/// and do something to affect the selection.
/// <see cref="ISelectionDisplayer"/>
/// </summary>
public interface IHasSelectContent : IIsSetting
{
    RenderFragment? GenerateSelectedContent();
    Task ClearSelectAsync();
}