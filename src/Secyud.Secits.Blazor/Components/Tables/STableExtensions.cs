using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public static class STableExtensions
{
    public static RenderFragment GenerateTableRow<TItem>(this SSelectorBase<STable<TItem>, TItem> selector,
        TItem item, RenderFragment<TItem> content) => builder =>
    {
        builder.OpenElement(0, "tr");
        if (selector.IsSelected(item))
        {
            builder.AddAttribute(1, "class", "selected");
        }

        builder.AddAttribute(2, "onclick",
            async () => await selector.OnSelectionActivateAsync(item));
        builder.AddContent(3, content(item));
        builder.CloseElement();
    };
}