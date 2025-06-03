namespace Secyud.Secits.Blazor.Arguments;

public class SelectionItem(object? item, string? text)
{
    public object? Item { get; } = item;
    public string? Text { get; } = text;

    public static SelectionItem FromObject(object? item, string? format)
    {
        if (item is null) return new SelectionItem(null, null);
        if (format is null) return new SelectionItem(item, item.ToString());
        return new SelectionItem(item, string.Format($"{{0:{format}}}", item));
    }

    public static SelectionItem FromObject<TItem>(TItem? item, Func<TItem?, string?> func)
    {
        return new SelectionItem(item, func(item));
    }
}