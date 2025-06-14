using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorPager<TItem> : IIteratorRenderer<TItem>, IContentFooterRenderer
{
    private List<TItem> _items = [];

    private int _totalCount;

    protected override void ApplySetting()
    {
        Master.ItemsRenderer.Apply(this);
        Master.Footer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ItemsRenderer.Forgo(this);
        Master.Footer.Forgo(this);
    }

    public async Task RefreshAsync()
    {
        if (Master.DataSource.Get() is not {} source) return;
        var result = await source.GetDataAsync(Master.DataRequest);
        _items = Enumerable.ToList<TItem>(result.Items);
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