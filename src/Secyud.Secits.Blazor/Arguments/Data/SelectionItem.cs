namespace Secyud.Secits.Blazor.Arguments;

public class SelectionItem(object? item, string? text)
{
    public object? Item { get; } = item;
    public string? Text { get; } = text;
}