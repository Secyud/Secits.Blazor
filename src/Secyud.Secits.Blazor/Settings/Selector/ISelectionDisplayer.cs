namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// selection displayer is a field to display
/// the selection.  
/// </summary>
public interface ISelectionDisplayer
{
    SSetting<IHasSelectContent> Selector { get; }
}