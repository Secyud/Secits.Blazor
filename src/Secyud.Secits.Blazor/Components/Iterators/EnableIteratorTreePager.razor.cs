using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorTreePager<TValue> : IGridColumnRenderer<TValue>, IExtendClassStyleBuilder
    where TValue : class, ITreeItem<TValue>
{
    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public Func<TValue, Task>? Children { get; set; }

    [Parameter]
    public ColumnPosition Position { get; set; }

    private string? _closeIcon;
    private string? _openIcon;

    public int RealWidth { get; set; } = 50;
    public int MaxWidth => 50;
    public int MinWidth => 50;

    public void BuildExtendClassStyle(ClassStyleContext context)
    {
        context.AppendClass("s-tree");
    }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        _closeIcon ??= IconProvider.GetIcon(IconName.CaretUp);
        _openIcon ??= IconProvider.GetIcon(IconName.CaretDown);
    }

    protected async Task OnToggleTreeItem(TValue item)
    {
        item.IsExpended = !item.IsExpended;

        if (item.Children is null)
        {
            if (Children is not null)
                await Children(item);
        }

        await InvokeAsync(StateHasChanged);
    }

    public RenderFragment GenerateHeader() => builder => { };
    public RenderFragment GenerateFooter() => builder => { };

    protected RenderFragment GenerateItems(RenderFragment<TValue> itemTemplate, List<TValue> values, int depth) =>
        builder =>
        {
            for (var i = 0; i < values.Count; i++)
            {
                var item = values[i];
                item.Depth = depth;
                builder.AddContent(i * 2, itemTemplate(item));
                if (item is { IsExpended: true, Children: { Count: > 0 } children })
                {
                    builder.AddContent(i * 2 + 1,
                        GenerateItems(itemTemplate, children, depth + 1));
                }
            }
        };
}