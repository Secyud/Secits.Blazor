using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorPager<TValue> : IIteratorRenderer<TValue>, IContentFooterRenderer
{
    private List<TValue> _items = [];

    private int _totalCount;

    [Parameter]
    public int[]? PageSizes { get; set; }

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
        Master.Footer.Apply(this);

        if (PageSizes is { Length: > 0 })
            Master.DataRequest.PageSize = PageSizes[0];
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
        Master.Footer.Forgo(this);
    }

    public async Task RefreshAsync()
    {
        if (Master.DataSource.Get() is not { } source) return;
        var result = await source.GetDataAsync(Master.DataRequest);
        _items = result.Items.ToList();
        _totalCount = result.TotalCount;
    }

    private async Task PageSizeChangedAsync(int pageSize)
    {
        Master.DataRequest.PageSize = pageSize;
        await Master.RefreshAsync();
    }

    private async Task PageIndexChangedAsync(int pageIndex)
    {
        Master.DataRequest.PageIndex = pageIndex;
        await Master.RefreshAsync();
    }
}